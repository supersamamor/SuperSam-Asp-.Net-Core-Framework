using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Extensions
{
    public static class StringExtensions
    {
        public static SelectList ToSelectList(this IEnumerable<string> values) =>
            new SelectList(values.Map(e => new SelectListItem(e, e)), "Value", "Text");
    }
}