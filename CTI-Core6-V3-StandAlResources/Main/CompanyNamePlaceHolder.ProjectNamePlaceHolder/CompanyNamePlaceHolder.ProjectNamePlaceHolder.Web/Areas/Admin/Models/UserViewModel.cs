using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models
{
    public record UserViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

      
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; } = "";

      
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; } = "";

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string Name { get; set; } = "";
        [Required]
        [Display(Name = "Entity")]
        public string EntityId { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "Status")]
        public bool IsActive { get; set; } = false;
        public SelectList Entities { get; set; } = new(new List<SelectListItem>());
        public IList<UserRoleViewModel> Roles { get; set; } = new List<UserRoleViewModel>();
        public SelectList Statuses { get; set; } = AdminUtilities.GetUserStatusList();
        public string? Entity { get; set; }
        public bool IsView { get; set; } 
        public bool IsEdit { get; set; }
    }

}
