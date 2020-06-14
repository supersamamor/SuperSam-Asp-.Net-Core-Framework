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
        public bool IdentityEmailConfirmed { get; set; }    
        public string IdentityUserName { get; set; }
    }
}
