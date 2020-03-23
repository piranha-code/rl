using System;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Type type)
        where TAttribute : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttribute), false)
                ?.Cast<TAttribute>()
                ?.FirstOrDefault();
        }
    }
}