using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static List<Type> GetDbSets(this DbContext context)
        {
            return context.GetType()
                .GetProperties()
                .Where(m => m.PropertyType.IsGenericType && typeof(DbSet<>).IsAssignableFrom(m.PropertyType.GetGenericTypeDefinition()))
                .Select(m => m.PropertyType.GetGenericArguments()[0])
                .ToList();
        }
    }
}