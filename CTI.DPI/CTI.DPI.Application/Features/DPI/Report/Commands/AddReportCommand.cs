using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DPI.Application.Features.DPI.Report.Commands;

public record AddReportCommand : ReportState, IRequest<Validation<Error, ReportState>>;

public class AddReportCommandHandler : BaseCommandHandler<ApplicationContext, ReportState, AddReportCommand>, IRequestHandler<AddReportCommand, Validation<Error, ReportState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ReportState>> Handle(AddReportCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddReport(request, cancellationToken));


	public async Task<Validation<Error, ReportState>> AddReport(AddReportCommand request, CancellationToken cancellationToken)
	{
		var error = Core.Helpers.SQLValidatorHelper.Validate(request.QueryString) ;
        if (!string.IsNullOrEmpty(error))
        {
            return Error.New(error);
        }
        ReportState entity = Mapper.Map<ReportState>(request);
		UpdateReportTableList(entity);
		UpdateReportTableJoinParameterList(entity);
		UpdateReportColumnHeaderList(entity);
		UpdateReportFilterGroupingList(entity);
		UpdateReportQueryFilterList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportState>(entity);
	}
	
	private void UpdateReportTableList(ReportState entity)
	{
		if (entity.ReportTableList?.Count > 0)
		{
			foreach (var reportTable in entity.ReportTableList!)
			{
				Context.Entry(reportTable).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportTableJoinParameterList(ReportState entity)
	{
		if (entity.ReportTableJoinParameterList?.Count > 0)
		{
			foreach (var reportTableJoinParameter in entity.ReportTableJoinParameterList!)
			{
				Context.Entry(reportTableJoinParameter).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportColumnHeaderList(ReportState entity)
	{
		if (entity.ReportColumnHeaderList?.Count > 0)
		{
			foreach (var reportColumnHeader in entity.ReportColumnHeaderList!)
			{
				Context.Entry(reportColumnHeader).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportFilterGroupingList(ReportState entity)
	{
		if (entity.ReportFilterGroupingList?.Count > 0)
		{
			foreach (var reportFilterGrouping in entity.ReportFilterGroupingList!)
			{
				Context.Entry(reportFilterGrouping).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportQueryFilterList(ReportState entity)
	{
		if (entity.ReportQueryFilterList?.Count > 0)
		{
			foreach (var reportQueryFilter in entity.ReportQueryFilterList!)
			{
				Context.Entry(reportQueryFilter).State = EntityState.Added;
			}
		}
	}
	
	private async Task AddApprovers(string reportId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Report).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = reportId,
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
					var userListWithRole = await (from a in _identityContext.Users
													join b in _identityContext.UserRoles on a.Id equals b.UserId
													join c in _identityContext.Roles on b.RoleId equals c.Id
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
			await Context.AddAsync(approvalRecord, cancellationToken);
		}
	}
}

public class AddReportCommandValidator : AbstractValidator<AddReportCommand>
{
    readonly ApplicationContext _context;

    public AddReportCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Report with id {PropertyValue} already exists");
        RuleFor(x => x.ReportName).MustAsync(async (reportName, cancellation) => await _context.NotExists<ReportState>(x => x.ReportName == reportName, cancellationToken: cancellation)).WithMessage("Report with reportName {PropertyValue} already exists");
	
    }
}
