using System.ComponentModel.DataAnnotations;

namespace Template.Web.Models.Template
{
    public class TemplateModel : BaseModel
    {
        [Display(Name = "LabelCode", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        [Display(Name = "LabelName", ResourceType = typeof(Resource))]
        public string Name { get; set; }
    }
}
