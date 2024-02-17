using System.ComponentModel.DataAnnotations;

namespace ProjectNamePlaceHolder.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}