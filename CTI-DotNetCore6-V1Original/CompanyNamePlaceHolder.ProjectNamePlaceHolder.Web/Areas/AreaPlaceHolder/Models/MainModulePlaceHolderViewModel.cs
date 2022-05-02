using System.ComponentModel.DataAnnotations;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models
{
    public record MainModulePlaceHolderViewModel : BaseAreaPlaceHolderViewModel
    {
        [Display(Name = "Code")]
        [Required]
        [StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
        public string Code { get; init; } = "";   
    }
}
