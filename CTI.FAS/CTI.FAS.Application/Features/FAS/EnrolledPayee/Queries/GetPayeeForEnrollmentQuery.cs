using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;
using CTI.FAS.Core.Constants;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;

public record GetPayeeForEnrollmentQuery : IRequest<IList<EnrolledPayeeState>>
{ 
    public string? Entity { get; set; }
}

public class GetPayeeForEnrollmentQueryHandler : IRequestHandler<GetPayeeForEnrollmentQuery, IList<EnrolledPayeeState>>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;

    public GetPayeeForEnrollmentQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<IList<EnrolledPayeeState>> Handle(GetPayeeForEnrollmentQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from enrolledPayee in _context.EnrolledPayee
                     where enrolledPayee.Status == EnrollmentStatus.New || enrolledPayee.Status == EnrollmentStatus.ForReEnrollment
                     select enrolledPayee)
                            .AsNoTracking();
        if (!string.IsNullOrEmpty(request.Entity))
        {
            query = query.Where(l => l.CompanyId == request.Entity);
        }
        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            query = from a in query
                    where a.LastModifiedBy == _authenticatedUser.UserId
                    select a;
        }
        return await query.Include(l => l.Company).ThenInclude(l => l!.DatabaseConnectionSetup).Include(l => l.Creditor)
                    .AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }
}
