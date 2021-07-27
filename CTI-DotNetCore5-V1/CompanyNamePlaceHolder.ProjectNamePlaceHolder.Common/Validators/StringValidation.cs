using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using LanguageExt;
using System;
using System.Linq;
using System.Net.Mail;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Validators
{
    public static partial class Validators
    {
        public static Func<string, Validation<Error, string>> NotLongerThan(int maxLength) =>
            str => Optional(str)
                .Where(s => s.Length <= maxLength)
                .ToValidation<Error>($"{str} must not be longer than {maxLength}");

        public static Func<string, Validation<Error, string>> ValidPhoneNumber(string countryCode) =>
            s =>
            {
                return Try(() =>
                {
                    var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                    var phoneNumber = phoneNumberUtil.Parse(s, countryCode);
                    return phoneNumberUtil.IsValidNumber(phoneNumber) ? s : "Not a valid phone number";
                }).ToValidation<string, Error>(_ => "Not a valid phone number");
            };

        public static Validation<Error, string> NotEmpty(string str) =>
            Optional(str)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToValidation<Error>("Empty string");

        public static Validation<Error, string> ValidNumber(this string s) =>
            s.All(char.IsNumber) ? Success<Error, string>(s) : "Not numeric";

        public static Validation<Error, string> ValidEmail(this string s) =>
            Try(() => { _ = new MailAddress(s); return s; }).ToValidation<string, Error>(_ => "Not a valid email");
    }
}
