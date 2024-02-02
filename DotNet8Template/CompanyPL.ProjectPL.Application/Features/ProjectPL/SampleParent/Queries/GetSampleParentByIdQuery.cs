using CompanyPL.Common.Core.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Queries;

public record GetSampleParentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SampleParentState>>;

public class GetSampleParentByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SampleParentState, GetSampleParentByIdQuery>, IRequestHandler<GetSampleParentByIdQuery, Option<SampleParentState>>
{
    public GetSampleParentByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
