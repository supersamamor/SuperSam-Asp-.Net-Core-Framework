using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Queries;

public record GetUploadProcessorByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UploadProcessorState>>;

public class GetUploadProcessorByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UploadProcessorState, GetUploadProcessorByIdQuery>, IRequestHandler<GetUploadProcessorByIdQuery, Option<UploadProcessorState>>
{
    public GetUploadProcessorByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
