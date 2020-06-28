using System.ComponentModel.DataAnnotations;

namespace SubComponentPlaceHolder.Data.Models
{
    public class MainModulePlaceHolder : BaseEntity
    {
        [StringLength(20)]
        [Required]
        public string Code { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(450)]
        public string MainModulePlaceHolderId { get; set; }
    }
}
