using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.Common.Utility.Extensions;

/// <summary>
/// Extension methods for <see cref="Validation{FAIL, SUCCESS}"/>.
/// </summary>
public static class ValidationExtensions
{
    /// <summary>
    /// Given a collection of <see cref="Validation"/>&lt;<see cref="Error"/>, <typeparamref name="T"/>&gt;,
    /// collect all errors to <see cref="Error"/>. If there are no errors,
    /// return SUCCESS.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validations">A collection of <see cref="Validation"/>&lt;<see cref="Error"/>, <typeparamref name="T"/>&gt;</param>
    /// <param name="success">The value to return if there are no errors</param>
    /// <returns></returns>
    public static Validation<Error, T> HarvestErrors<T>(this IEnumerable<Validation<Error, T>> validations, T success) where T : class
    {
        var errors = validations.Bind(v => v.Match(_ => None, errors => Some(errors))) //IEnumerable<Seq<Error>>
                                .Bind(e => e) //IEnumerable<Error>
                                .ToSeq(); // Seq<Error>
        return errors.Count == 0
            ? Success<Error, T>(success)
            : Validation<Error, T>.Fail(errors);
    }
}