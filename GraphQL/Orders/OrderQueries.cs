using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MSA2021.Data;
using MSA2021.Model;
using MSA2021.Extensions;

namespace MSA2021.GraphQL.Orders
{
    [ExtendObjectType(name: "Query")]
    public class OrderQueries
    {
        [UseAppDbContext]
       
        public IQueryable<Order> GetOrders([ScopedService] AppDbContext context)
        {
            return context.Orders;
        }

        [UseAppDbContext]
        public Order GetOrder(int id, [ScopedService] AppDbContext context)
        {
            return context.Orders.Find(id);
        }
    }

}

