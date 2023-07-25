using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using CTI.Common.Core.Queries;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.Approval.Queries;

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
