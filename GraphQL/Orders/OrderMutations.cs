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

namespace MSA2021.GraphQL.Orders
{
    [ExtendObjectType(name: "Mutation")]
    public class OrderMutations
    {
        [UseAppDbContext]
        public async Task<Order> AddUserAsync(AddOrderInput input,
[ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = input.Id,
                UserName = input.UserName,
                ProductID = input.ProductID,
                Quantity = input.Quantity,
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);

            return order;
        }

        [UseAppDbContext]
        public async Task<Order> EditAsync(EditOrderInput input,
                [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var order = await context.Orders.FindAsync(input.Id);

            order.UserName = input.UserName ?? order.UserName;
            order.ProductID = input.ProductID ?? order.ProductID;
            order.Quantity = input.Quantity ?? order.Quantity;

            await context.SaveChangesAsync(cancellationToken);

            return order;
        }

    }
}
