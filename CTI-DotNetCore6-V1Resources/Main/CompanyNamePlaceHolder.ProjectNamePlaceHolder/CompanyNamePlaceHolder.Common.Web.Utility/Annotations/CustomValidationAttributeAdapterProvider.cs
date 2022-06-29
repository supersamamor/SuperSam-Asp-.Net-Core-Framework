using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Annotations
{
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider =
            new ValidationAttributeAdapterProvider();

        public IAttributeAdapter? GetAttributeAdapter(ValidationAttribute attribute,
            IStringLocalizer? stringLocalizer) =>
            attribute switch
            {
                LessThanEqualAttribute lessThanEqualAttribute => new LessThanEqualAttributeAdapter(lessThanEqualAttribute, stringLocalizer),
                GreaterThanEqualAttribute greaterThanEqualAttribute => new GreaterThanEqualAttributeAdapter(greaterThanEqualAttribute, stringLocalizer),
                _ => baseProvider.GetAttributeAdapter(attribute, stringLocalizer)
            };
    }
}