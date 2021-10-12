using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSA2021.GraphQL.Orders
{
    public record EditOrderInput(
      int Id,
      string? UserName,
      int? ProductID,
      int? Quantity);
}
