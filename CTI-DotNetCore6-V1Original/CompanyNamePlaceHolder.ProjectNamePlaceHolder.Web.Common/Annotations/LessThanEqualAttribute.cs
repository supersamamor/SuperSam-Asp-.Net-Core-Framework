using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Annotations
{
    public class LessThanEqualAttribute : BaseValidationAttribute
    {
        public LessThanEqualAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        public string OtherProperty { get; private set; }
        public string? OtherPropertyDisplayName { get; set; }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = "{0} must be less than or equal to {1}";
            }
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name,
                                 OtherPropertyDisplayName ?? OtherProperty);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Unknown property {OtherProperty}");
            }
            OtherPropertyDisplayName = GetDisplayNameForProperty(otherPropertyInfo);
            var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (((IComparable)value).CompareTo(otherPropertyValue) > 0)
            {
                return new(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }

    public class LessThanEqualAttributeAdapter : AttributeAdapterBase<LessThanEqualAttribute>
    {
        public LessThanEqualAttributeAdapter(LessThanEqualAttribute attribute,
                                                    IStringLocalizer stringLocalizer) 
            : base(attribute, stringLocalizer)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-lessThanEqual", GetErrorMessage(context));
            var property = Attribute.OtherProperty;
            MergeAttribute(context.Attributes, "data-val-lessThanEqual-other", property);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            Attribute.OtherPropertyDisplayName = validationContext.MetadataProvider.GetMetadataForProperties(validationContext.ModelMetadata.ContainerType!)
                                                                                   .FirstOrDefault(x => x.Name == Attribute.OtherProperty)?.DisplayName;
            return Attribute.FormatErrorMessage(validationContext.ModelMetadata.DisplayName);
        }
    }
}
