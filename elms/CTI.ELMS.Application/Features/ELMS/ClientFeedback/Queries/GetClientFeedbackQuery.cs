using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ClientFeedback.Queries;

public record GetClientFeedbackQuery : BaseQuery, IRequest<PagedListResponse<ClientFeedbackState>>
{
    public string? LeadTaskId { get; set; }
}

public class GetClientFeedbackQueryHandler : BaseQueryHandler<ApplicationContext, ClientFeedbackState, GetClientFeedbackQuery>, IRequestHandler<GetClientFeedbackQuery, PagedListResponse<ClientFeedbackState>>
{
    public GetClientFeedbackQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<ClientFeedbackState>> Handle(GetClientFeedbackQuery request, CancellationToken cancellationToken = default)
    {
        var query = Context.ClientFeedback.AsNoTracking();
        if (!string.IsNullOrEmpty(request.LeadTaskId))
        {
            query = from a in query
                    join b in Context.LeadTaskClientFeedBack on a.Id equals b.ClientFeedbackId
                    where b.LeadTaskId == request.LeadTaskId
                    select a;
        }
        return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
            request.SortColumn, request.SortOrder,
            request.PageNumber, request.PageSize,
            cancellationToken);
    }
}
