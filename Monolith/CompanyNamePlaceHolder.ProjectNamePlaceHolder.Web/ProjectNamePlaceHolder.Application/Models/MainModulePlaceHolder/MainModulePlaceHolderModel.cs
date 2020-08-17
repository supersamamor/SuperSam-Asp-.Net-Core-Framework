using System.ComponentModel.DataAnnotations;

namespace ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder
{
    public class MainModulePlaceHolderModel : BaseModel
    {
        [Display(Name = "LabelCode", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PromptMessageFieldIsRequired")]
        public string Code { get; set; }
        [Display(Name = "LabelName", ResourceType = typeof(Resource))]
        public string Name { get; set; }
    }
}
