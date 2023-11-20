using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskMaster.Queries;

public record GetTaskMasterQuery : BaseQuery, IRequest<PagedListResponse<TaskMasterState>>;

public class GetTaskMasterQueryHandler : BaseQueryHandler<ApplicationContext, TaskMasterState, GetTaskMasterQuery>, IRequestHandler<GetTaskMasterQuery, PagedListResponse<TaskMasterState>>
{
    public GetTaskMasterQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
