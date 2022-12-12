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
        public DateTime? DateFrom { get; set; } 
        public DateTime? DateTo { get; set; }
        public string? BankId { get; set; }
        public string? BatchId { get; set; }
        public string? DownloadUrl { get; set; }
        public bool DisplayGenerateButton { get; set; }
        public bool DisplayRevokeButton { get; set; }        
        public bool ShowBatchFilter { get; set; }
        public bool ShowBankFilter { get; set; }
        public string? ProccessButtonLabel { get; set; } = "Generate";
    }
}
