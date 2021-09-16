using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions
{
    public static class EnumUtilities
    {
        public static SelectList ToSelectList<T>() => 
            Enum.GetNames(typeof(T)).ToList().ToSelectList();
    }
}
