using System.ComponentModel.DataAnnotations;

namespace Template.Data.Models
{
    public class Template : BaseEntity
    {
        [StringLength(20)]
        [Required]
        public string Code { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
    }
}
