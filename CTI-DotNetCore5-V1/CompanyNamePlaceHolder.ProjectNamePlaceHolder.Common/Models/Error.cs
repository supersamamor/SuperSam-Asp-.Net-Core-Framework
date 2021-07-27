using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models
{
    public class Error : NewType<Error, string>
    {
        public Error(string str) : base(str) { }
        public static implicit operator Error(string str) => New(str);
    }

    public static class ErrorExtensions
    {
        public static Error Join(this Seq<Error> errors) => string.Join("; ", errors);
    }
}
