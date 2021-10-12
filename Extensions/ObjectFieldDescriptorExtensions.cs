using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Types;

namespace MSA2021.Extensions
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UseAppDbContext<TDbContext>(
    this IObjectFieldDescriptor descriptor)
    where TDbContext : DbContext
        {
            return descriptor.UseScopedService<TDbContext>(
                create: s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
                disposeAsync: (s, c) => c.DisposeAsync());
        }
    }
}
