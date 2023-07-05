using LanguageExt;
using LanguageExt.Common;
using System.Net.Mail;
using static LanguageExt.Prelude;

namespace CNPlaceHolder.Common.Utility.Validators;

public static partial class Validators
{
    /// <summary>
    /// Validates a string and returns SUCCESS if it's length is not longer than the provided <paramref name="maxLength"/>.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static Validation<Error, string> NotLongerThan(this string str, int maxLength) =>
        Optional(str).Where(s => s.Length <= maxLength)
                     .ToValidation<Error>($"{str} must not be longer than {maxLength}");

    /// <summary>
    /// Validates a string and returns SUCCESS if it represents a valid phone number for the specified country code.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    public static Validation<Error, string> ValidPhoneNumber(this string s, string countryCode) =>
        Try(() =>
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(s, countryCode);
            return phoneNumberUtil.IsValidNumber(phoneNumber) ? s : "Not a valid phone number";
        }).ToValidation<string, Error>(_ => "Not a valid phone number");

    /// <summary>
    /// Validates a string and returns SUCCESS if it's not empty, null or consists solely of whitespace.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Validation<Error, string> NotEmpty(this string str) =>
        Optional(str).Where(s => !string.IsNullOrWhiteSpace(s))
                     .ToValidation<Error>("Empty string");

    /// <summary>
    /// Validates a string and returns SUCCESS if it represents a valid number.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Validation<Error, string> ValidNumber(this string s) =>
        s.All(char.IsNumber) ? Success<Error, string>(s) : "Not numeric";

    /// <summary>
    /// Validates a string and returns SUCCESS if it represents a valid email address.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Validation<Error, string> ValidEmail(this string s) =>
        Try(() => { _ = new MailAddress(s); return s; }).ToValidation<string, Error>(_ => "Not a valid email");
}