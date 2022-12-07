using CTI.FAS.Application.Features.FAS.Company.Queries;
using CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;
using CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;
using CTI.FAS.Core.Constants;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.PaymentTransaction;

[Authorize(Policy = Permission.PaymentTransaction.View)]
public class SentTransactionsModel : BasePageModel<SentTransactionsModel>
{
    [BindProperty]
    public IList<NewPaymentTransactionViewModel> NewPaymentTransactionList { get; set; } = new List<NewPaymentTransactionViewModel>();

    [BindProperty]
    public PaymentTransactionFilterModel Filter { get; set; } = new();
    public PaymentTransactionTabNavigationPartial PaymentTransactionTabNavigation { get; set; } = new() { TabName = Constants.PaymentTransactionTabNavigation.Sent };

    public async Task<IActionResult> OnGet(string? handler, string? entity, string? paymentType, string? accountTransaction, DateTime? dateFrom, DateTime? dateTo, string? batchId, string? downloadUrl)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await FetchTransactionDetails(handler, entity, paymentType, accountTransaction, dateFrom, dateTo, batchId, downloadUrl);
    }

    public async Task<IActionResult> OnPost(string? handler)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }         
        return Page();
    }
    private async Task<IActionResult> FetchTransactionDetails(string? handler, string? entity, string? paymentType, string? accountTransaction, DateTime? dateFrom, DateTime? dateTo, string? batchId, string? downloadUrl)
    {
        ModelState.Clear();
        Filter.ShowBatchFilter = true;
        Filter.ProccessButtonLabel = "";
        if (string.IsNullOrEmpty(entity))
        {
            entity = (await Mediatr.Send(new GetCompanyQuery())).Data.ToList().FirstOrDefault()?.Id;
        }
        PaymentTransactionTabNavigation.SetEntity(entity);
        Filter.Entity = entity;
        Filter.PaymentType = paymentType;
        Filter.AccountTransaction = accountTransaction;
        Filter.DateFrom = dateFrom;
        Filter.DateTo = dateTo;
        Filter.BatchId = batchId;
        Filter.DownloadUrl = downloadUrl;
        var query = new GetPaymentTransactionWithCustomFilterQuery()
        {
            Status = PaymentTransactionStatus.Sent,
            Entity = entity,
            PaymentType = paymentType,
            AccountTransaction = accountTransaction,
            DateFrom = dateFrom,
            DateTo = dateTo,
            BatchId = batchId,
        };
        if (handler == "Retrieve")
        {
            if (!string.IsNullOrEmpty(entity))
            {
                if (Filter.ShowBatchFilter && string.IsNullOrEmpty(batchId) && dateFrom == null && dateFrom == null)
                {
                    NotyfService.Warning(Localizer["Please select batch or date filters."]);
                    return Page();
                }
                else if (string.IsNullOrEmpty(batchId) && (dateFrom == null || dateTo == null))
                {
                    NotyfService.Warning(Localizer["Invalid date filters. Check `date from` and `date to` fields."]);
                    return Page();
                }
            }
        }
        if (!string.IsNullOrEmpty(batchId) || dateFrom != null || dateFrom != null)
        {
            NewPaymentTransactionList = Mapper.Map<IList<NewPaymentTransactionViewModel>>(await Mediatr.Send(query));
        }
        Filter.DisplayGenerateButton = false;
        Filter.DisplayRevokeButton = false;
        return Page();
    }  
}
