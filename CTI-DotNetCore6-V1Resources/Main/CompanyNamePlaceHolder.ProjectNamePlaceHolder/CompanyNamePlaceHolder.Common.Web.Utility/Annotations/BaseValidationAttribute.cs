using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Annotations
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