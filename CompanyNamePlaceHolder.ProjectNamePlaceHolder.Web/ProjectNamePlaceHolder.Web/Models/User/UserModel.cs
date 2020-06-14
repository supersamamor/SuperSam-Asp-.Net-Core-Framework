using System.ComponentModel.DataAnnotations;

namespace ProjectNamePlaceHolder.Web.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        [Display(Name = "LabelName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PromptMessageFieldIsRequired")]
        public string FullName { get; set; }  
        public string IdentityId { get; set; }
        [Display(Name = "LabelActivated", ResourceType = typeof(Resource))]
        public bool IdentityEmailConfirmed { get; set; }
        [Display(Name = "LabelEmail", ResourceType = typeof(Resource))]
        public string IdentityEmail { get; set; }
        [Display(Name = "LabelUserName", ResourceType = typeof(Resource))]
        public string IdentityUserName { get; set; }
    }
}
