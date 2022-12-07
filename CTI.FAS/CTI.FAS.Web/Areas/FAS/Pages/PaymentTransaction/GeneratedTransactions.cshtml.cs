using CTI.FAS.Application.Features.FAS.Company.Queries;
using CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;
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
public class GeneratedTransactionsModel : BasePageModel<GeneratedTransactionsModel>
{
    [BindProperty]
    public IList<NewPaymentTransactionViewModel> NewPaymentTransactionList { get; set; } = new List<NewPaymentTransactionViewModel>();
  
    [BindProperty]
    public PaymentTransactionFilterModel Filter { get; set; } = new();
    public PaymentTransactionTabNavigationPartial PaymentTransactionTabNavigation { get; set; } = new() { TabName = Constants.PaymentTransactionTabNavigation.Generated };
   
    public async Task<IActionResult> OnGet(string? entity, string? paymentType, string? accountTransaction, DateTime? dateFrom, DateTime? dateTo, string? downloadUrl)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (string.IsNullOrEmpty(entity))
        {
            entity = (await Mediatr.Send(new GetCompanyQuery())).Data.ToList().FirstOrDefault()?.Id;
        }
        ModelState.Clear();
        Filter.Entity = entity;
        Filter.PaymentType = paymentType;
        Filter.AccountTransaction = accountTransaction;
        Filter.DateFrom = dateFrom;
        Filter.DateTo = dateTo;
        var query = new GetPaymentTransactionWithCustomFilterQuery()
        {
            Status = PaymentTransactionStatus.Generated,
            Entity = entity,
            PaymentType = paymentType,
            AccountTransaction = accountTransaction,
            DateFrom = dateFrom,
            DateTo = dateTo,
        };
        if (!string.IsNullOrEmpty(entity))
        { NewPaymentTransactionList = Mapper.Map<IList<NewPaymentTransactionViewModel>>(await Mediatr.Send(query)); }
        Filter.DisplayGenerateButton = NewPaymentTransactionList.Count > 0;
        Filter.DownloadUrl = downloadUrl;
        return Page();
    }
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var selectedNewPaymentTransactionList = NewPaymentTransactionList.Where(l => l.Enabled).Select(l => l.Id).ToList();
        if (selectedNewPaymentTransactionList.Count == 0)
        {
            NotyfService.Warning(Localizer["Please select atleast 1 payment transaction to process."]);
            return Page();
        }
        try
        {
            var downloadUrl = await Mediatr.Send(new ProcessPaymentCommand(selectedNewPaymentTransactionList));
            NotyfService.Success(Localizer["Generation success."]);
            return RedirectToPage("NewTransactions", new
            {
                downloadUrl,
                Filter.Entity,
                Filter.PaymentType,
                Filter.AccountTransaction,
                Filter.DateFrom,
                Filter.DateTo,
            });
        }
        catch (Exception ex)
        {
            NotyfService.Error(Localizer["An error has ocurred, please contact System administrator."]);
            Logger.LogError(ex, "Exception encountered");
        }
        return Page();
    }
}
