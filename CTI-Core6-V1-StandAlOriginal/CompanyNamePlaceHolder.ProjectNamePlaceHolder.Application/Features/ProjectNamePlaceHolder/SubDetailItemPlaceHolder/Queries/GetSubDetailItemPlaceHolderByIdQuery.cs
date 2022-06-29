using CTI.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Queries;

public record GetSubDetailItemPlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SubDetailItemPlaceHolderState>>;

public class GetSubDetailItemPlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SubDetailItemPlaceHolderState, GetSubDetailItemPlaceHolderByIdQuery>, IRequestHandler<GetSubDetailItemPlaceHolderByIdQuery, Option<SubDetailItemPlaceHolderState>>
{
    public GetSubDetailItemPlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
