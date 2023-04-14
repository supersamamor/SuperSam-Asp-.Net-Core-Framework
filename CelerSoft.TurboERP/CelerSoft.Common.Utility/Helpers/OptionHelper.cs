using LanguageExt;

namespace CelerSoft.Common.Utility.Helpers;

/// <summary>
/// Helper methods related to <see cref="Option{A}"/>.
/// </summary>
public static class OptionHelper
{
    /// <summary>
    /// Returns an <see cref="Option{A}"/> depending on whether
    /// <paramref name="value"/> is null or not.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<A> ToOption<A>(A? value) where A : class =>
            value ?? Option<A>.None;
}