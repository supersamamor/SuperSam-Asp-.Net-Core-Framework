using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Extensions
{
    public static class EnumUtilities
    {
        public static SelectList ToSelectList<T>() =>
            Enum.GetValues(typeof(T)).Cast<T>().ToSelectList();

        public static SelectList ToSelectList<T>(T selectedValue) =>
            Enum.GetValues(typeof(T)).Cast<T>().ToSelectList(selectedValue);

        public static SelectList ToSelectList<T>(this IEnumerable<T> enums) =>
            new SelectList(enums.ToSelectListItems(), "Value", "Text");

        public static SelectList ToSelectList<T>(this IEnumerable<T> enums, T selectedValue) =>
            new SelectList(enums.ToSelectListItems(), "Value", "Text", selectedValue!.ToString());

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> enums) =>
            enums.Select(
                x => new SelectListItem
                {
                    Text = ((Enum)(object)x!).ToDescription(),
                    Value = x!.ToString()
                });

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

        public static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}