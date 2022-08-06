using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record TenantPOSSalesViewModel : BaseViewModel
{
    [Display(Name = "Sales Type")]
    [Range(0, 2)]
    public int SalesType { get; init; }
    [Display(Name = "HourCode")]
    [Range(0, 23)]
    public int HourCode { get; init; }
    [Display(Name = "Sales Category")]
    [StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
    [Required]
    public string? SalesCategory { get; init; }
    [Display(Name = "Sales Date")]
    [Required]
    public DateTime SalesDate { get; init; } = DateTime.Now.Date;
    [Display(Name = "Manually Checked")]
    public bool IsAutoCompute { get; init; }
    [Display(Name = "Sales Amount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal SalesAmount { get; init; }
    [Display(Name = "Old Accumulated Total")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal OldAccumulatedTotal { get; init; }
    [Display(Name = "New Accumulated Total")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal NewAccumulatedTotal { get; init; }
    [Display(Name = "Taxable Sales Amount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal TaxableSalesAmount { get; init; }
    [Display(Name = "Non Taxable Sales Amount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal NonTaxableSalesAmount { get; init; }
    [Display(Name = "Senior Citizen Discount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal SeniorCitizenDiscount { get; init; }
    [Display(Name = "Promo Discount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal PromoDiscount { get; init; }
    [Display(Name = "Other Discount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal OtherDiscount { get; init; }
    [Display(Name = "Refund Discount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal RefundDiscount { get; init; }
    [Display(Name = "Void Amount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal VoidAmount { get; init; }
    [Display(Name = "Adjustment Amount")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal AdjustmentAmount { get; init; }
    [Display(Name = "Total Service Charge")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal TotalServiceCharge { get; init; }
    [Display(Name = "Total Tax")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal TotalTax { get; init; }
    [Display(Name = "No Of Sales Transactions")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal NoOfSalesTransactions { get; init; }
    [Display(Name = "No Of Transactions")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal NoOfTransactions { get; init; }
    [Display(Name = "Total Net Sales")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal TotalNetSales { get; init; }
    [Display(Name = "Control Number")]


    public int ControlNumber { get; init; }
    [Display(Name = "File Name")]
    [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? FileName { get; init; }
    [Display(Name = "TenantPOS")]
    [Required]

    public string TenantPOSId { get; init; } = "";
    public string? ForeignKeyTenantPOS { get; set; }
    [Display(Name = "Validation Status")]
    [Required]

    public int ValidationStatus { get; init; }
    [Display(Name = "Validation Remarks")]
    [StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? ValidationRemarks { get; init; }
    [Display(Name = "Autocalculated New Accumulated Total")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal AutocalculatedNewAccumulatedTotal { get; init; }
    [Display(Name = "Autocalculated Old Accumulated Total")]
    [Required]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal AutocalculatedOldAccumulatedTotal { get; init; }
    [Display(Name = "Autocalculated Total Net Sales")]
    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal AutoCalculatedTotalNetSales { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public TenantPOSViewModel? TenantPOS { get; init; }
    public string? TenantId { get; init; }
    public int SetDayNumber(DateTime dateFrom)
    {
        return (int)((this.SalesDate - dateFrom).TotalDays) + 1;
    } 
    public string TenantPOSCode { get; init; } = "";
    public string TenantName { get; init; } = "";
    public string TenantCode { get; init; } = "";
    public string UpdatedByName { get; init; } = "";
    public string UpdatedDate { get; init; } = "";
}
