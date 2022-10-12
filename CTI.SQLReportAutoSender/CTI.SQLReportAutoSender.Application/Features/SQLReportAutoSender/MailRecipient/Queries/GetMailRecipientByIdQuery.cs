using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Queries;

public record GetMailRecipientByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<MailRecipientState>>;

public class GetMailRecipientByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, MailRecipientState, GetMailRecipientByIdQuery>, IRequestHandler<GetMailRecipientByIdQuery, Option<MailRecipientState>>
{
    public GetMailRecipientByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<MailRecipientState>> Handle(GetMailRecipientByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.MailRecipient.Include(l=>l.Report)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
