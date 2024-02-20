using LanguageExt;
using LanguageExt.Common;

namespace CompanyNamePlaceHolder.Common.Utility.Extensions;

/// <summary>
/// Extension methods for <see cref="Error"/>
/// </summary>
public static class ErrorExtensions
{
    /// <summary>
    /// Flattens a collection of <see cref="Error"/>
    /// using the provided delimiter.
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static Error Join(this Seq<Error> errors, string delimiter = ";") => string.Join(delimiter, errors.Map(e => e.ToString()));
}