using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Web.Controllers;

namespace Web.ApplicationParts
{
    public class GenericControllerApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        public IEnumerable<TypeInfo> Types { get; }
        public override string Name => "GenericControllers";

        public GenericControllerApplicationPart(ApiVersion version)
        {
            var builder = CreateODataModelBuilder(version, Assembly.GetExecutingAssembly());
            var types = new List<TypeInfo>();

            if (builder.EntitySets.Any(m => !m.EntityType.ClrType.IsEnum))
            {
                types.AddRange(builder.EntitySets
                    .Where(m => !m.EntityType.ClrType.IsEnum)
                    .Select(m => new { m.EntityType.ClrType, Key = m.EntityType.Keys.Select(m => m?.PropertyInfo?.PropertyType) })
                    .Select(m => typeof(GenericController<,>).MakeGenericType(m.ClrType, m.Key.FirstOrDefault()).GetTypeInfo()));
            }

            if (builder.EntitySets.Any(m => m.EntityType.ClrType.IsEnum))
            {
                types.AddRange(builder.EntitySets
                    .Where(m => m.EntityType.ClrType.IsEnum)
                    .Select(m => m.EntityType.ClrType)
                    .Select(m => typeof(EnumController<>).MakeGenericType(m).GetTypeInfo()));
            }

            //
            /*if (builder.StructuralTypes.Any(m => !m.ClrType.IsEnum))
            {
                types.AddRange(builder.StructuralTypes
                    .Where(m => !m.ClrType.IsEnum)
                    .Select(m => m.ClrType)
                    .Select(m => typeof(GenericController<,>).MakeGenericType(m, typeof(int)).GetTypeInfo()));
            }

            if (builder.StructuralTypes.Any(m => m.ClrType.IsEnum))
            {
                types.AddRange(builder.StructuralTypes
                    .Where(m => m.ClrType.IsEnum)
                    .Select(m => m.ClrType)
                    .Select(m => typeof(EnumController<>).MakeGenericType(m).GetTypeInfo()));
            }*/

            //
            if (builder.EnumTypes.Any(m => m.ClrType.IsEnum))
            {
                types.AddRange(builder.EnumTypes
                    .Where(m => m.ClrType.IsEnum)
                    .Select(m => m.ClrType)
                    .Select(m => typeof(EnumController<>).MakeGenericType(m).GetTypeInfo()));
            }

            //
            /*foreach (var type in builder.StructuralTypes)
            {
                if (typeof(ISecurityEntity).IsAssignableFrom(type.ClrType))
                {
                    type.AddCollectionProperty(typeof(ISecurityEntity).GetProperty("BlockedMembers"));
                }
            }*/

            Types = types.Distinct();
        }

        protected virtual ODataModelBuilder CreateODataModelBuilder(ApiVersion version, Assembly assembly)
        {
            var builder = new ODataModelBuilder();

            assembly.GetTypes()
                .Where(m => !m.IsAbstract && m.GetInterface(nameof(IModelConfiguration)) != null)
                .Select(m => (IModelConfiguration)Activator.CreateInstance(m))
                .ToList()
                .ForEach(m => m.Apply(builder, version));

            return builder;
        }
    }
}