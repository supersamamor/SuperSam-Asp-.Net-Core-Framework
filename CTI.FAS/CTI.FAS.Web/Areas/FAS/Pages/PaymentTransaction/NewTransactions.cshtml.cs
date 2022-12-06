using CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;
using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Pages.PaymentTransaction;

[Authorize(Policy = Permission.PaymentTransaction.View)]
public class NewTransactionsModel : BasePageModel<NewTransactionsModel>
{
    [BindProperty]
    public IList<NewPaymentTransactionViewModel> NewPaymentTransactionList { get; set; } = new List<NewPaymentTransactionViewModel>();
    [BindProperty]
    public string? DownloadUrl { get; set; }
    public PaymentTransactionTabNavigationPartial PaymentTransactionTabNavigation { get; set; } = new() { TabName = Constants.PaymentTransactionTabNavigation.New };
    [BindProperty]
    [Required]
    public string? Entity { get; set; }
    [BindProperty]
    [Required]
    public string? PaymentType { get; set; }
    [BindProperty]
    [Required]
    public string? AccountTransaction { get; set; }
    [BindProperty]
    [Required]
    public DateTime? DateFrom { get; set; }
    [BindProperty]
    [Required]
    public DateTime? DateTo { get; set; }
    public async Task<IActionResult> OnGet(string? entity, string? paymentType, string? accountTransaction, DateTime? dateFrom, DateTime? dateTo, string? downloadUrl)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Entity = entity;
        PaymentType = paymentType;
        AccountTransaction = accountTransaction;
        DateFrom = dateFrom;
        DateTo = dateTo;
        var query = new GetNewPaymentTransactionQuery()
        {
            Status = PaymentTransactionStatus.New,
            Entity = entity,
            PaymentType = paymentType,
            AccountTransaction = accountTransaction,
            DateFrom = dateFrom,
            DateTo = dateTo,
        };
        if (!string.IsNullOrEmpty(entity))
        { NewPaymentTransactionList = Mapper.Map<IList<NewPaymentTransactionViewModel>>(await Mediatr.Send(query)); }
        DownloadUrl = downloadUrl;
        return Page();
    }
    public IActionResult OnPost()
    {
        return Page();
    }
}
