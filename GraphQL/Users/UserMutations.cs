using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MSA2021.Model;
using MSA2021.Data;
using MSA2021.Extensions;
using System.Threading;
using Octokit;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;

namespace MSA2021.GraphQL.Users
{
    [ExtendObjectType(name: "Mutation")]
    public class UserMutations
    {



        [UseAppDbContext]
        [Authorize]
        public async Task<Model.User> EditStudentAsync(EditUserInput input, ClaimsPrincipal claimsPrincipal,
                [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var studentIdStr = claimsPrincipal.Claims.First(c => c.Type == "studentId").Value;
            var student = await context.Users.FindAsync(int.Parse(studentIdStr), cancellationToken);

            student.Username = input.Username ?? student.Username;
            student.Password = input.Password ?? student.Password;

            await context.SaveChangesAsync(cancellationToken);

            return student;
        }


        [UseAppDbContext]
        public async Task<LoginPayload> LoginAsync(LoginInput input, [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var client = new GitHubClient(new ProductHeaderValue("iStore"));
            var request = new OauthTokenRequest(Startup.Configuration["Github:ClientId"], Startup.Configuration["Github:ClientSecret"], input.Code);
            var tokenInfo = await client.Oauth.CreateAccessToken(request);


            if (tokenInfo.AccessToken == null)
            {
                throw new GraphQLRequestException(ErrorBuilder.New()
                    .SetMessage("Bad code")
                    .SetCode("AUTH_NOT_AUTHENTICATED")
                    .Build());
            }

            client.Credentials = new Credentials(tokenInfo.AccessToken);
            var user = await client.User.Current();

            var u = await context.Users.FirstOrDefaultAsync(s => s.Username == user.Login, cancellationToken);

            if (u == null)
            {
                u = new Model.User
                {
                    Username = user.Name ?? user.Login,
                    Password = user.Login,
                };

                context.Users.Add(u);
                await context.SaveChangesAsync(cancellationToken);
            }

            // authentication successful so generate jwt token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>{
                 new Claim("Username", u.Username),
              };

            var jwtToken = new JwtSecurityToken(
                "iStore",
                "iStore",
                claims,
                expires: DateTime.Now.AddDays(90),
                signingCredentials: credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            Console.WriteLine(token);

            return new LoginPayload(u, token);


        }

    }
}
