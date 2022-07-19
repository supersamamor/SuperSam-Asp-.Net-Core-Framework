using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Queries;

public record GetSubDetailItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SubDetailItemState>>;

public class GetSubDetailItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SubDetailItemState, GetSubDetailItemByIdQuery>, IRequestHandler<GetSubDetailItemByIdQuery, Option<SubDetailItemState>>
{
    public GetSubDetailItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
