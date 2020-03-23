using Infrastructure.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Web.Configurations
{
    public class CategoryConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            var category = builder.EntitySet<Categories>(nameof(Categories)).EntityType;
            category.HasKey(p => p.CategoryId);
        }
    }
}