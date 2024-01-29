using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using CompanyPL.Common.Core.Queries;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Approval.Queries;

public record GetApproverSetupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ApproverSetupState>>;

public class GetApproverSetupByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ApproverSetupState, GetApproverSetupByIdQuery>, IRequestHandler<GetApproverSetupByIdQuery, Option<ApproverSetupState>>
{
    public GetApproverSetupByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }

    public override async Task<Option<ApproverSetupState>> Handle(GetApproverSetupByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.ApproverSetup
            .Include(l => l.ApproverAssignmentList)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}
