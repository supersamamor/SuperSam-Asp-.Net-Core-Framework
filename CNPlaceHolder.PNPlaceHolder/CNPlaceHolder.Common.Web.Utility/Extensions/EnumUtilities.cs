using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.ComponentModel;

namespace CNPlaceHolder.Common.Web.Utility.Extensions;

/// <summary>
/// Utility methods and extension methods for enumeration types.
/// </summary>
public static class EnumUtilities
{
    /// <summary>
    /// Converts the values of the specified enumeration <typeparamref name="T"/> to a <see cref="SelectList"/>.
    /// </summary>
    /// <typeparam name="T">An enumeration type</typeparam>
    /// <returns></returns>
    public static SelectList ToSelectList<T>() =>
        Enum.GetValues(typeof(T)).Cast<T>().ToSelectList();

    /// <summary>
    /// Converts the values of the specified enumeration <typeparamref name="T"/> to a <see cref="SelectList"/>.
    /// The option with the value matching <paramref name="selectedValue"/> will be selected by default.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selectedValue">The option with value equal to this will be selected by default</param>
    /// <returns></returns>
    public static SelectList ToSelectList<T>(T selectedValue) =>
        Enum.GetValues(typeof(T)).Cast<T>().ToSelectList(selectedValue);

    /// <summary>
    /// Converts the values of <see cref="IEnumerable"/>&lt;<typeparamref name="T"/>&gt; to a <see cref="SelectList"/>.
    /// </summary>
    /// <typeparam name="T">An enumeration type</typeparam>
    /// <param name="enums"></param>
    /// <returns></returns>
    public static SelectList ToSelectList<T>(this IEnumerable<T> enums) =>
        new(enums.ToSelectListItems(), "Value", "Text");

    /// <summary>
    /// Converts the values of <see cref="IEnumerable"/>&lt;<typeparamref name="T"/>&gt; to a <see cref="SelectList"/>.
    /// The option with the value matching <paramref name="selectedValue"/> will be selected by default.
    /// </summary>
    /// <typeparam name="T">An enumeration type</typeparam>
    /// <param name="enums"></param>
    /// <param name="selectedValue">The option with value equal to this will be selected by default</param>
    /// <returns></returns>
    public static SelectList ToSelectList<T>(this IEnumerable<T> enums, T selectedValue) =>
        new(enums.ToSelectListItems(), "Value", "Text", selectedValue!.ToString());

    /// <summary>
    /// Converts the values of <see cref="IEnumerable"/>&lt;<typeparamref name="T"/>&gt; to an <see cref="IEnumerable"/>&lt;<see cref="SelectListItem"/>&gt;.
    /// </summary>
    /// <typeparam name="T">An enumeration type</typeparam>
    /// <param name="enums"></param>
    /// <returns></returns>
    public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> enums) =>
        enums.Select(
            x => new SelectListItem
            {
                Text = ((Enum)(object)x!).ToDescription(),
                Value = x!.ToString()
            });

    /// <summary>
    /// Converts the values of <see cref="IEnumerable"/>&lt;<typeparamref name="T"/>&gt; to an <see cref="IEnumerable"/>&lt;<see cref="SelectListItem"/>&gt;.
    /// The option with the value matching <paramref name="selectedValue"/> will be selected by default.
    /// </summary>
    /// <typeparam name="T">An enumeration type</typeparam>
    /// <param name="enums"></param>
    /// <param name="selectedValue">The option with value equal to this will be selected by default</param>
    /// <returns></returns>
    public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> enums, T selectedValue)
    {
        return enums.Select(
            x => new SelectListItem
            {
                Text = ((Enum)(object)x!).ToDescription(),
                Value = x!.ToString(),
                Selected = selectedValue != null && selectedValue.Equals(x)
            });
    }

    /// <summary>
    /// Gets the value of the <see cref="DescriptionAttribute"/> for the specified <see cref="Enum"/> value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToDescription(this Enum value)
    {
        var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}