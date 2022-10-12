using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Queries;

public record GetMailSettingByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<MailSettingState>>;

public class GetMailSettingByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, MailSettingState, GetMailSettingByIdQuery>, IRequestHandler<GetMailSettingByIdQuery, Option<MailSettingState>>
{
    public GetMailSettingByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<MailSettingState>> Handle(GetMailSettingByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.MailSetting.Include(l=>l.Report)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
