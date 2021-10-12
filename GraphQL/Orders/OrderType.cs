using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using MSA2021.Data;
using MSA2021.Model;
using MSA2021.GraphQL.Users;
using System.Threading;


namespace MSA2021.GraphQL.Orders
{
    public class OrderType : ObjectType<Order>
    {
        protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
        {
            descriptor.Field(p => p.Id).Type<NonNullType<IdType>>();
            descriptor.Field(s => s.UserName).Type<NonNullType<StringType>>();
            descriptor.Field(s => s.ProductID).Type<NonNullType<IntType>>();
            descriptor.Field(s => s.Quantity).Type<NonNullType<IntType>>();

            descriptor
                .Field(s => s.User)
                .ResolveWith<Resolvers>(r => r.GetUsers(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<OrderType>>>>();

        }

        private class Resolvers
        {
            public async Task<IEnumerable<User>> GetUsers(Order order, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return (IEnumerable<User>)await context.Users.FindAsync(new object[] { order.UserName }, cancellationToken);
            }

        }

    }
}
