using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Infrastructure.Extensions;

namespace Web.Attributes
{
    public class GenericControllerAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var type = controller.ControllerType.GetGenericArguments().FirstOrDefault();
            if (type != null)
            {
                controller.ControllerName = type.GetAttribute<DescriptionAttribute>()
                    ?.Description
                    ?? type.Name;
            }
        }
    }
}