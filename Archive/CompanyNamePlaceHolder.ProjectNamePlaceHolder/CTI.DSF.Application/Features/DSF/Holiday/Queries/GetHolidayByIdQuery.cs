using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Holiday.Queries;

public record GetHolidayByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<HolidayState>>;

public class GetHolidayByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, HolidayState, GetHolidayByIdQuery>, IRequestHandler<GetHolidayByIdQuery, Option<HolidayState>>
{
    public GetHolidayByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
