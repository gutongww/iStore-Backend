using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using MSA2021.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace MSA2021.Extensions
{
    public class UseAppDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
    IDescriptorContext context,
    IObjectFieldDescriptor descriptor,
    MemberInfo member)
        {
            descriptor.UseDbContext<AppDbContext>();
        }

    }
}
