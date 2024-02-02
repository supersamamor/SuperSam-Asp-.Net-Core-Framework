using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Helpers
{
    public static class ApprovalHelper
    {
        public static async Task AddApprovers(ApplicationContext context, IdentityContext identityContext, string approvalModule, string recordId,
            CancellationToken cancellationToken)
        {
            var approverList = await context.ApproverAssignment.Include(l => l.ApproverSetup)
                .Where(l => l.ApproverSetup.TableName == approvalModule).AsNoTracking().ToListAsync(cancellationToken);
            if (approverList.Count > 0)
            {
                var approvalRecord = new ApprovalRecordState()
                {
                    ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
                    DataId = recordId,
                    ApprovalList = new List<ApprovalState>()
                };
                foreach (var approverItem in approverList)
                {
                    if (approverItem.ApproverType == ApproverTypes.User)
                    {
                        var approval = new ApprovalState()
                        {
                            Sequence = approverItem.Sequence,
                            ApproverUserId = approverItem.ApproverUserId!,
                        };
                        if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
                        {
                            approval.EmailSendingStatus = SendingStatus.Pending;
                        }
                        approvalRecord.ApprovalList.Add(approval);
                    }
                    else if (approverItem.ApproverType == ApproverTypes.Role)
                    {
                        var userListWithRole = await (from a in identityContext.Users
                                                      join b in identityContext.UserRoles on a.Id equals b.UserId
                                                      join c in identityContext.Roles on b.RoleId equals c.Id
                                                      where c.Id == approverItem.ApproverRoleId
                                                      select a.Id).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        foreach (var userId in userListWithRole)
                        {
                            var approval = new ApprovalState()
                            {
                                Sequence = approverItem.Sequence,
                                ApproverUserId = userId,
                            };
                            if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
                            {
                                approval.EmailSendingStatus = SendingStatus.Pending;
                            }
                            approvalRecord.ApprovalList.Add(approval);
                        }
                    }
                }
                await context.AddAsync(approvalRecord, cancellationToken);
            }
        }
        public static async Task<string> GetApprovalStatus(ApplicationContext context, string dataId, CancellationToken cancellationToken)
        {
            string? approvalStatus = await (from a in context.ApprovalRecord
                                            where a.DataId == dataId
                                            select a.Status).FirstOrDefaultAsync(cancellationToken);
            switch (approvalStatus)
            {
                case ApprovalStatus.New:
                    return @"<span class=""badge bg-secondary"">" + approvalStatus + "</span>";
                case ApprovalStatus.ForApproval:
                    return @"<span class=""badge bg-info"">" + approvalStatus + "</span>";
                case ApprovalStatus.PartiallyApproved:
                    return @"<span class=""badge bg-primary"">" + approvalStatus + "</span>";
                case ApprovalStatus.Approved:
                    return @"<span class=""badge bg-success"">" + approvalStatus + "</span>";
                case ApprovalStatus.Rejected:
                    return @"<span class=""badge bg-danger"">" + approvalStatus + "</span>";
                default:
                    break;
            }
            return approvalStatus ?? "";
        }
    }
}
