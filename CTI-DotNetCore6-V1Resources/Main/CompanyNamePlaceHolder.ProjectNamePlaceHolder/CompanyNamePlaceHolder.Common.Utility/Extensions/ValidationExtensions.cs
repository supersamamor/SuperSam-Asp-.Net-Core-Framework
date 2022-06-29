using LanguageExt;
using LanguageExt.Common;
using System.Collections.Generic;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.Common.Utility.Extensions
{
    public static class ValidationExtensions
    {
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
}