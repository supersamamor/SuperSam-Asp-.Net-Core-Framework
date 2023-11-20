using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Holiday.Queries;

public record GetHolidayQuery : BaseQuery, IRequest<PagedListResponse<HolidayState>>;

public class GetHolidayQueryHandler : BaseQueryHandler<ApplicationContext, HolidayState, GetHolidayQuery>, IRequestHandler<GetHolidayQuery, PagedListResponse<HolidayState>>
{
    public GetHolidayQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
