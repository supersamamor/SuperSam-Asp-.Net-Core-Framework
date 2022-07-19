using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Queries;

public record GetSubDetailListByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SubDetailListState>>;

public class GetSubDetailListByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SubDetailListState, GetSubDetailListByIdQuery>, IRequestHandler<GetSubDetailListByIdQuery, Option<SubDetailListState>>
{
    public GetSubDetailListByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
