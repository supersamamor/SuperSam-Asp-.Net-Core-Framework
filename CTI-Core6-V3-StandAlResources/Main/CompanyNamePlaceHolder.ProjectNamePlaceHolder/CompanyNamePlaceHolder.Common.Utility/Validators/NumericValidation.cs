using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.Common.Utility.Validators;

/// <summary>
/// A class that contains common validation functions.
/// </summary>
public static partial class Validators
{
    /// <summary>
    /// Validates a number and returns SUCCESS if it's greater than or equal to the provided <paramref name="minimum"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="minimum"></param>
    /// <returns></returns>
    public static Validation<Error, decimal> AtLeast(this decimal value, decimal minimum) =>
        Optional(value).Where(d => d >= minimum)
                       .ToValidation<Error>($"Must be greater or equal to {minimum}");

    /// <summary>
    /// Validates a number and returns SUCCESS if it's less than or equal to the provided <paramref name="max"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Validation<Error, decimal> AtMost(this decimal value, decimal max) =>
        Optional(value).Where(d => d <= max)
                       .ToValidation<Error>($"Must be less than or equal to {max}");
}