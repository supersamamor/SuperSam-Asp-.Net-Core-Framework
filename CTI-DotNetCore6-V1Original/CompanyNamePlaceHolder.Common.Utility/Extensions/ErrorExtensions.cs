using LanguageExt;
using LanguageExt.Common;

namespace CompanyNamePlaceHolder.Common.Utility.Extensions
{
    public static class ErrorExtensions
    {
        public static Error Join(this Seq<Error> errors) => string.Join("; ", errors.Map(e => e.ToString()));
    }
}