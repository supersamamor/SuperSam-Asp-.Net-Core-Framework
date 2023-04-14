using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Extensions;
using CelerSoft.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Approval.Queries;

public record GetPendingApprovalsQuery() : BaseQuery, IRequest<PagedListResponse<PendingApproval>>
{
    public string TableName { get; set; } = "";
}

public class GetPendingApprovalsQueryHandler : IRequestHandler<GetPendingApprovalsQuery, PagedListResponse<PendingApproval>>
{
    private readonly ApplicationContext _context;
    public GetPendingApprovalsQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public virtual async Task<PagedListResponse<PendingApproval>> Handle(GetPendingApprovalsQuery request, CancellationToken cancellationToken = default)
    {
        var query = from a in _context.Approval
                    join b in _context.ApprovalRecord on a.ApprovalRecordId equals b.Id
                    where (a.Status == ApprovalStatus.ForApproval || a.Status == ApprovalStatus.PartiallyApproved)
                    && (a.EmailSendingStatus == SendingStatus.Failed || a.EmailSendingStatus == SendingStatus.Done)
					&& b.ApproverSetup!.TableName == request.TableName
                    select new PendingApproval()
                    {
                        DataId = b.DataId,
                        ApprovalId = a.Id,
                        ApprovalStatus = a.Status,
                        EmailSendingStatus = a.EmailSendingStatus,
                        EmailSendingRemarks = a.EmailSendingRemarks,
                        EmailSendingDateTime = a.EmailSendingDateTime,
                        LastModifiedDate = a.LastModifiedDate,
                        RecordName = GetRecordName(_context, request.TableName, b.DataId),
                    };   
        return await query.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                 request.SortColumn, request.SortOrder,
                                                                 request.PageNumber, request.PageSize,
                                                                 cancellationToken);
    }    
    private static string? GetRecordName(ApplicationContext context, string? tableName, string? dataId)
    {
        string? recordName = "";
		if(tableName == ApprovalModule.PurchaseRequisition)
		{
			recordName = context.PurchaseRequisition.Where(l => l.Id == dataId).AsNoTracking().FirstOrDefault()?.Id;
		}
		if(tableName == ApprovalModule.SupplierQuotation)
		{
			recordName = context.SupplierQuotation.Where(l => l.Id == dataId).AsNoTracking().FirstOrDefault()?.Id;
		}
		if(tableName == ApprovalModule.Inventory)
		{
			recordName = context.Inventory.Where(l => l.Id == dataId).AsNoTracking().FirstOrDefault()?.Id;
		}
		if(tableName == ApprovalModule.Order)
		{
			recordName = context.Order.Where(l => l.Id == dataId).AsNoTracking().FirstOrDefault()?.Code;
		}
		        
        return recordName;
    }
}
public record PendingApproval
{
    public string DataId { get; set; } = "";
    public string? RecordName { get; set; } = "";
    public string ApprovalId { get; set; } = "";
    public string ApprovalStatus { get; set; } = "";
    public string EmailSendingStatus { get; set; } = "";
    public string EmailSendingRemarks { get; set; } = "";
    public DateTime? EmailSendingDateTime { get; set; }
    public DateTime LastModifiedDate { get; set; }
}