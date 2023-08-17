using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Holiday.Queries;

public record GetHolidayByDateQuery(DateTime Date) : IRequest<bool>;

public class GetHolidayByDateQueryHandler :  IRequestHandler<GetHolidayByDateQuery, bool>
{
    private readonly ApplicationContext _context;
    public GetHolidayByDateQueryHandler(ApplicationContext context) 
    {
        _context = context;
    }

    public async Task<bool> Handle(GetHolidayByDateQuery request, CancellationToken cancellationToken = default)
    {
       return await _context.Holiday.Where(l => l.HolidayDate == request.Date.Date).AnyAsync(cancellationToken: cancellationToken);
    }
}
