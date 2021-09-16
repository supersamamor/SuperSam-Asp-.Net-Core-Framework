using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Annotations
{
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider =
            new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
            attribute switch
            {
                LessThanEqualAttribute lessThanEqualAttribute => new LessThanEqualAttributeAdapter(lessThanEqualAttribute, stringLocalizer),
                GreaterThanEqualAttribute greaterThanEqualAttribute => new GreaterThanEqualAttributeAdapter(greaterThanEqualAttribute, stringLocalizer),
                _ => baseProvider.GetAttributeAdapter(attribute, stringLocalizer)
            };
    }
}
