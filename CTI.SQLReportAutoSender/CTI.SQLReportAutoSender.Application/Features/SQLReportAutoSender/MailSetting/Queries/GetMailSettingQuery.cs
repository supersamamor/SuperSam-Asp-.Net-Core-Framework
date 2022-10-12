using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Queries;

public record GetMailSettingQuery : BaseQuery, IRequest<PagedListResponse<MailSettingState>>;

public class GetMailSettingQueryHandler : BaseQueryHandler<ApplicationContext, MailSettingState, GetMailSettingQuery>, IRequestHandler<GetMailSettingQuery, PagedListResponse<MailSettingState>>
{
    public GetMailSettingQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<MailSettingState>> Handle(GetMailSettingQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<MailSettingState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
