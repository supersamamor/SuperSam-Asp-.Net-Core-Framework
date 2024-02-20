using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Tags.Queries;

public record GetTagsQuery : BaseQuery, IRequest<PagedListResponse<TagsState>>;

public class GetTagsQueryHandler : BaseQueryHandler<ApplicationContext, TagsState, GetTagsQuery>, IRequestHandler<GetTagsQuery, PagedListResponse<TagsState>>
{
    public GetTagsQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
