using System.ComponentModel;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Web.Configurations
{
    public class RolesConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            var role = builder.AddEnumType(typeof(Role));
            role.Name = typeof(Role).GetAttribute<DescriptionAttribute>()
                ?.Description 
                ?? nameof(Role);
        }
    }
}