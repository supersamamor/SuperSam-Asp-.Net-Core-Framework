using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Annotations;

/// <summary>
/// Adapter provider for custom-defined attributes.
/// </summary>
public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
{
    private readonly IValidationAttributeAdapterProvider baseProvider =
        new ValidationAttributeAdapterProvider();

    /// <summary>
    /// Provides adapter for specified <paramref name="attribute"/>.
    /// </summary>
    /// <param name="attribute">Attribute for which to provide the adapter for</param>
    /// <param name="stringLocalizer"></param>
    /// <returns>The attribute provider</returns>
    public IAttributeAdapter? GetAttributeAdapter(ValidationAttribute attribute,
        IStringLocalizer? stringLocalizer) =>
        attribute switch
        {
            LessThanAttribute lessThanAttribute => new LessThanAttributeAdapter(lessThanAttribute, stringLocalizer),
            LessThanEqualAttribute lessThanEqualAttribute => new LessThanEqualAttributeAdapter(lessThanEqualAttribute, stringLocalizer),
            GreaterThanAttribute greaterThanAttribute => new GreaterThanAttributeAdapter(greaterThanAttribute, stringLocalizer),
            GreaterThanEqualAttribute greaterThanEqualAttribute => new GreaterThanEqualAttributeAdapter(greaterThanEqualAttribute, stringLocalizer),
            _ => baseProvider.GetAttributeAdapter(attribute, stringLocalizer)
        };
}