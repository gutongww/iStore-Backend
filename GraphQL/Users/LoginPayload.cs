using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSA2021.Model;

namespace MSA2021.GraphQL.Users
{
    public record LoginPayload
    (
        User user,
        string jwt);
}
