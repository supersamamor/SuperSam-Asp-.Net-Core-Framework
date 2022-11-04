using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Queries;

public record GetReportTableCollectionDetailByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportTableCollectionDetailState>>;

public class GetReportTableCollectionDetailByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportTableCollectionDetailState, GetReportTableCollectionDetailByIdQuery>, IRequestHandler<GetReportTableCollectionDetailByIdQuery, Option<ReportTableCollectionDetailState>>
{
    public GetReportTableCollectionDetailByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportTableCollectionDetailState>> Handle(GetReportTableCollectionDetailByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportTableCollectionDetail.Include(l=>l.IFCATenantInformation).Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
