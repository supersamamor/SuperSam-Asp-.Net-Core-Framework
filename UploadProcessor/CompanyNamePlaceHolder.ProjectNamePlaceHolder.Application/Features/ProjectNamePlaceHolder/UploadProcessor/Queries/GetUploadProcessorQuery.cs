using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Queries;

public record GetUploadProcessorQuery : BaseQuery, IRequest<PagedListResponse<UploadProcessorState>>;

public class GetUploadProcessorQueryHandler : BaseQueryHandler<ApplicationContext, UploadProcessorState, GetUploadProcessorQuery>, IRequestHandler<GetUploadProcessorQuery, PagedListResponse<UploadProcessorState>>
{
    public GetUploadProcessorQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
