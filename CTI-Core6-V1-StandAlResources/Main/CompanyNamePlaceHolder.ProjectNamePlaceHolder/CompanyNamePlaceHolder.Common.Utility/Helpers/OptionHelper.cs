using LanguageExt;

namespace CompanyNamePlaceHolder.Common.Utility.Helpers
{
    public static class OptionHelper
    {
        public static Option<A> ToOption<A>(A? value) where A : class =>
                value ?? Option<A>.None;
    }
}