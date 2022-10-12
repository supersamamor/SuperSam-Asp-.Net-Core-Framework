using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Queries;

public record GetMailRecipientQuery : BaseQuery, IRequest<PagedListResponse<MailRecipientState>>;

public class GetMailRecipientQueryHandler : BaseQueryHandler<ApplicationContext, MailRecipientState, GetMailRecipientQuery>, IRequestHandler<GetMailRecipientQuery, PagedListResponse<MailRecipientState>>
{
    public GetMailRecipientQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<MailRecipientState>> Handle(GetMailRecipientQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<MailRecipientState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
