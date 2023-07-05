using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CNPlaceHolder.Common.Web.Utility.Annotations;

/// <summary>
/// A base class for validation attributes.
/// </summary>
public abstract class BaseValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Get value of <see cref="DisplayAttribute"/> for <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The property for which you want to get the <see cref="DisplayAttribute"/> value of</param>
    /// <returns>The value of the <see cref="DisplayAttribute"/></returns>
    protected static string GetDisplayNameForProperty(PropertyInfo property)
    {
        var attribute = CustomAttributeExtensions.GetCustomAttributes(property, true)
                                                 .FirstOrDefault(a => a is DisplayAttribute);
        return attribute != null ? ((DisplayAttribute)attribute).Name ?? property.Name : property.Name;
    }
}