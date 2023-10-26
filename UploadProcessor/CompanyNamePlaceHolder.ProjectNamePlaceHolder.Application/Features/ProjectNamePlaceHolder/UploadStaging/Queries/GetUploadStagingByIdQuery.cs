using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Queries;

public record GetUploadStagingByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UploadStagingState>>;

public class GetUploadStagingByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UploadStagingState, GetUploadStagingByIdQuery>, IRequestHandler<GetUploadStagingByIdQuery, Option<UploadStagingState>>
{
    public GetUploadStagingByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UploadStagingState>> Handle(GetUploadStagingByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UploadStaging.Include(l=>l.UploadProcessor)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
