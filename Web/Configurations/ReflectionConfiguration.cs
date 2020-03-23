using System;
using System.Linq;
using System.Reflection;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Web.Configurations
{
    public class ReflectionConfiguration : IModelConfiguration
    {
        NorthwindContext _context = new NorthwindContext();

        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            var p = typeof(Infrastructure.Enums.Role).GetProperties();
            _context.GetDbSets().ForEach(entityType =>
            {
                if (entityType.IsEnum)
                {
                    var name = entityType.Name;//entityType.GetAttribute<DescriptionAttribute>().Description ?? entityType.Name;
                    if (builder.EnumTypes.FirstOrDefault(k => k.Name == name) == null)
                    {
                        builder.AddEnumType(entityType);
                    }
                }
                else if (builder.EntitySets.FirstOrDefault(k => k.Name == entityType.Name) == null)
                {
                    var primaryKeys = _context.ModelBuilder.Value.Entity(entityType).Metadata.FindPrimaryKey()
                        ?.Properties
                        ?.Select(m => m.PropertyInfo)
                        ?.ToList()
                        ?? entityType.GetProperties().Take(1).ToList()
                        ?? throw new ArgumentNullException();

                    if (primaryKeys != null)
                    {
                        var entity = builder.AddEntitySet(entityType.Name, builder.AddEntityType(entityType)).EntityType;
                        primaryKeys.ForEach(primaryKey => entity.HasKey(primaryKey));
                    }
                    /*else if (builder.StructuralTypes.FirstOrDefault(k => k.Name == entityType.Name) == null)
                    {
                        builder.AddComplexType(entityType);
                    }*/
                }
            });
        }
    }
}