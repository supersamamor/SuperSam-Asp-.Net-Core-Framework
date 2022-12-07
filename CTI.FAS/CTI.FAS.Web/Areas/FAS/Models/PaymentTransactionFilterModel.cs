using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models
{
    public class PaymentTransactionFilterModel
    {       
        [Required]
        public string? Entity { get; set; }      
        [Required]
        public string? PaymentType { get; set; }       
        [Required]
        public string? AccountTransaction { get; set; }      
        [Required]
        public DateTime? DateFrom { get; set; }       
        [Required]
        public DateTime? DateTo { get; set; }
        public string? DownloadUrl { get; set; }
        public bool DisplayGenerateButton { get; set; }
    }
}
