using CTI.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Queries;

public record GetSubDetailListPlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SubDetailListPlaceHolderState>>;

public class GetSubDetailListPlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SubDetailListPlaceHolderState, GetSubDetailListPlaceHolderByIdQuery>, IRequestHandler<GetSubDetailListPlaceHolderByIdQuery, Option<SubDetailListPlaceHolderState>>
{
    public GetSubDetailListPlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
