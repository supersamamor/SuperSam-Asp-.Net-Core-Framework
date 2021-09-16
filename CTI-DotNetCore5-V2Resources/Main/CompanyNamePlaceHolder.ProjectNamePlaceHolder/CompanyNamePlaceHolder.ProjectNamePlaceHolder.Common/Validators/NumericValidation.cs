using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using LanguageExt;
using System;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Validators
{
    public static partial class Validators
    {
        public static Func<decimal, Validation<Error, decimal>> AtLeast(decimal minimum) =>
            value => Optional(value)
                .Where(d => d >= minimum)
                .ToValidation<Error>($"Must be greater or equal to {minimum}");

        public static Func<decimal, Validation<Error, decimal>> AtMost(decimal max) =>
            value => Optional(value)
                .Where(d => d <= max)
                .ToValidation<Error>($"Must be less than or equal to {max}");
    }
}
