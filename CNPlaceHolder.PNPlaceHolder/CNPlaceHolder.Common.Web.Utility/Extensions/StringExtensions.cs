using Microsoft.AspNetCore.Mvc.Rendering;

namespace CNPlaceHolder.Common.Web.Utility.Extensions;

/// <summary>
/// Extension methods for the <see cref="String"/> class
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns a <see cref="SelectList"/> from a collection of strings
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static SelectList ToSelectList(this IEnumerable<string> values) =>
        new(values.Map(e => new SelectListItem(e, e)), "Value", "Text");
}