using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions
{
    public static class StringExtensions
    {
        public static SelectList ToSelectList(this IEnumerable<string> values) => 
            new(values.Map(e => new SelectListItem(e, e)), "Value", "Text");
    }
}
