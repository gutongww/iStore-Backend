using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using MSA2021.Data;
using MSA2021.Model;
using MSA2021.GraphQL.Orders;
using System.Threading;

namespace MSA2021.GraphQL.Users
{
    public class UserType : ObjectType<User>
    {

        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(s => s.Username).Type<NonNullType<StringType>>();
            descriptor.Field(s => s.Password).Type<NonNullType<StringType>>();
            descriptor.Field(s => s.Address).Type<NonNullType<StringType>>();

            descriptor
                .Field(s => s.Orders)
                .ResolveWith<Resolvers>(r => r.GetOrders(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<OrderType>>>>();


        }

        private class Resolvers
        {
            public async Task<IEnumerable<Order>> GetOrders(User user, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Orders.Where(c => c.UserName == user.Username).ToArrayAsync(cancellationToken);
            }

        }

    }
}
