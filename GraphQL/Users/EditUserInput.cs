using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSA2021.GraphQL.Users
{
    public record EditUserInput(
        string? Username,
        string? Password,
        string? Address);
}
