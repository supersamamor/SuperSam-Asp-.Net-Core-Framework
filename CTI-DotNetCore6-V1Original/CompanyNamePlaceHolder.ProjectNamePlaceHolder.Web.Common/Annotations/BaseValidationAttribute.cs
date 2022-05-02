using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Annotations
{
    public class BaseValidationAttribute : ValidationAttribute
    {
        protected static string GetDisplayNameForProperty(PropertyInfo property)
        {
            var attribute = CustomAttributeExtensions.GetCustomAttributes(property, true)
                                                     .FirstOrDefault(a => a is DisplayAttribute);
            return attribute != null ? ((DisplayAttribute)attribute).Name ?? property.Name : property.Name;
        }
    }
}
