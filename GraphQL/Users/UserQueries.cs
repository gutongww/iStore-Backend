using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MSA2021.Data;
using MSA2021.Model;
using MSA2021.Extensions;
using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;

namespace MSA2021.GraphQL.Users
{
    [ExtendObjectType(name: "Query")]
    public class UserQueries
    {
        [UseAppDbContext]
        public IQueryable<User> GetUsers([ScopedService] AppDbContext context)
        {
            return context.Users;
        }

        [UseAppDbContext]
        public User GetUser(string username, [ScopedService] AppDbContext context)
        {
            return context.Users.Find(username);
        }

        [UseAppDbContext]
        [Authorize]
        public User GetSelf(ClaimsPrincipal claimsPrincipal, [ScopedService] AppDbContext context)
        {
            var UserIdStr = claimsPrincipal.Claims.First(c => c.Type == "Username").Value;

            return context.Users.Find(int.Parse(UserIdStr));
        }



    }
}
