using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DPI.Application.Features.DPI.Report.Commands;

public record EditReportCommand : ReportState, IRequest<Validation<Error, ReportState>>;

public class EditReportCommandHandler : BaseCommandHandler<ApplicationContext, ReportState, EditReportCommand>, IRequestHandler<EditReportCommand, Validation<Error, ReportState>>
{
    public EditReportCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportState>> Handle(EditReportCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditReport(request, cancellationToken));


	public async Task<Validation<Error, ReportState>> EditReport(EditReportCommand request, CancellationToken cancellationToken)
	{
        var error = Core.Helpers.SQLValidatorHelper.Validate(request.QueryString);
        if (!string.IsNullOrEmpty(error))
        {
            return Error.New(error);
        }
        var entity = await Context.Report.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateReportTableList(entity, request, cancellationToken);
		await UpdateReportTableJoinParameterList(entity, request, cancellationToken);
		await UpdateReportColumnHeaderList(entity, request, cancellationToken);
		await UpdateReportFilterGroupingList(entity, request, cancellationToken);
		await UpdateReportQueryFilterList(entity, request, cancellationToken);
        await UpdateReportRoleAssignmentList(entity, request, cancellationToken);
        Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportState>(entity);
	}
	
	private async Task UpdateReportTableList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
	{
		IList<ReportTableState> reportTableListForDeletion = new List<ReportTableState>();
		var queryReportTableForDeletion = Context.ReportTable.Where(l => l.ReportId == request.Id).AsNoTracking();
		if (entity.ReportTableList?.Count > 0)
		{
			queryReportTableForDeletion = queryReportTableForDeletion.Where(l => !(entity.ReportTableList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportTableListForDeletion = await queryReportTableForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportTable in reportTableListForDeletion!)
		{
			Context.Entry(reportTable).State = EntityState.Deleted;
		}
		if (entity.ReportTableList?.Count > 0)
		{
			foreach (var reportTable in entity.ReportTableList.Where(l => !reportTableListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportTableState>(x => x.Id == reportTable.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportTable).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportTable).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateReportTableJoinParameterList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
	{
		IList<ReportTableJoinParameterState> reportTableJoinParameterListForDeletion = new List<ReportTableJoinParameterState>();
		var queryReportTableJoinParameterForDeletion = Context.ReportTableJoinParameter.Where(l => l.ReportId == request.Id).AsNoTracking();
		if (entity.ReportTableJoinParameterList?.Count > 0)
		{
			queryReportTableJoinParameterForDeletion = queryReportTableJoinParameterForDeletion.Where(l => !(entity.ReportTableJoinParameterList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportTableJoinParameterListForDeletion = await queryReportTableJoinParameterForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportTableJoinParameter in reportTableJoinParameterListForDeletion!)
		{
			Context.Entry(reportTableJoinParameter).State = EntityState.Deleted;
		}
		if (entity.ReportTableJoinParameterList?.Count > 0)
		{
			foreach (var reportTableJoinParameter in entity.ReportTableJoinParameterList.Where(l => !reportTableJoinParameterListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportTableJoinParameterState>(x => x.Id == reportTableJoinParameter.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportTableJoinParameter).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportTableJoinParameter).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateReportColumnHeaderList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
	{
		IList<ReportColumnHeaderState> reportColumnHeaderListForDeletion = new List<ReportColumnHeaderState>();
		var queryReportColumnHeaderForDeletion = Context.ReportColumnHeader.Where(l => l.ReportId == request.Id).AsNoTracking();
		if (entity.ReportColumnHeaderList?.Count > 0)
		{
			queryReportColumnHeaderForDeletion = queryReportColumnHeaderForDeletion.Where(l => !(entity.ReportColumnHeaderList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportColumnHeaderListForDeletion = await queryReportColumnHeaderForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportColumnHeader in reportColumnHeaderListForDeletion!)
		{
			Context.Entry(reportColumnHeader).State = EntityState.Deleted;
		}
		if (entity.ReportColumnHeaderList?.Count > 0)
		{
			foreach (var reportColumnHeader in entity.ReportColumnHeaderList.Where(l => !reportColumnHeaderListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportColumnHeaderState>(x => x.Id == reportColumnHeader.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportColumnHeader).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportColumnHeader).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateReportFilterGroupingList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
	{
		IList<ReportFilterGroupingState> reportFilterGroupingListForDeletion = new List<ReportFilterGroupingState>();
		var queryReportFilterGroupingForDeletion = Context.ReportFilterGrouping.Where(l => l.ReportId == request.Id).AsNoTracking();
		if (entity.ReportFilterGroupingList?.Count > 0)
		{
			queryReportFilterGroupingForDeletion = queryReportFilterGroupingForDeletion.Where(l => !(entity.ReportFilterGroupingList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportFilterGroupingListForDeletion = await queryReportFilterGroupingForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportFilterGrouping in reportFilterGroupingListForDeletion!)
		{
			Context.Entry(reportFilterGrouping).State = EntityState.Deleted;
		}
		if (entity.ReportFilterGroupingList?.Count > 0)
		{
			foreach (var reportFilterGrouping in entity.ReportFilterGroupingList.Where(l => !reportFilterGroupingListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportFilterGroupingState>(x => x.Id == reportFilterGrouping.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportFilterGrouping).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportFilterGrouping).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateReportQueryFilterList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
	{
		IList<ReportQueryFilterState> reportQueryFilterListForDeletion = new List<ReportQueryFilterState>();
		var queryReportQueryFilterForDeletion = Context.ReportQueryFilter.Where(l => l.ReportId == request.Id).AsNoTracking();
		if (entity.ReportQueryFilterList?.Count > 0)
		{
			queryReportQueryFilterForDeletion = queryReportQueryFilterForDeletion.Where(l => !(entity.ReportQueryFilterList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportQueryFilterListForDeletion = await queryReportQueryFilterForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportQueryFilter in reportQueryFilterListForDeletion!)
		{
			Context.Entry(reportQueryFilter).State = EntityState.Deleted;
		}
		if (entity.ReportQueryFilterList?.Count > 0)
		{
			foreach (var reportQueryFilter in entity.ReportQueryFilterList.Where(l => !reportQueryFilterListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportQueryFilterState>(x => x.Id == reportQueryFilter.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportQueryFilter).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportQueryFilter).State = EntityState.Modified;
				}
			}
		}
	}
    private async Task UpdateReportRoleAssignmentList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
    {
        IList<ReportRoleAssignmentState> reportRoleAssignmentListForDeletion = new List<ReportRoleAssignmentState>();
        var queryReportRoleAssignmentListForDeletion = Context.ReportRoleAssignment.Where(l => l.ReportId == request.Id).AsNoTracking();
        if (entity.ReportRoleAssignmentList?.Count > 0)
        {
            queryReportRoleAssignmentListForDeletion = queryReportRoleAssignmentListForDeletion.Where(l => !(entity.ReportRoleAssignmentList.Select(l => l.RoleName).ToList().Contains(l.RoleName)));
        }
        reportRoleAssignmentListForDeletion = await queryReportRoleAssignmentListForDeletion.ToListAsync(cancellationToken: cancellationToken);
        foreach (var reportRoleAssignment in reportRoleAssignmentListForDeletion!)
        {
            Context.Entry(reportRoleAssignment).State = EntityState.Deleted;
        }
        if (entity.ReportRoleAssignmentList?.Count > 0)
        {
            foreach (var reportRoleAssignment in entity.ReportRoleAssignmentList.Where(l => !reportRoleAssignmentListForDeletion.Select(l => l.RoleName).Contains(l.RoleName)).ToList())
            {
                var existingBusinessUnitAssignment = await Context.ReportRoleAssignment.Where(x => x.RoleName == reportRoleAssignment.RoleName && x.ReportId == request.Id)
                    .AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
                if (existingBusinessUnitAssignment == null)
                {
                    Context.Entry(reportRoleAssignment).State = EntityState.Added;
                }
                else
                {
                    Mapper.Map(existingBusinessUnitAssignment, reportRoleAssignment);
                    reportRoleAssignment.Id = existingBusinessUnitAssignment.Id;
                    Context.Entry(reportRoleAssignment).State = EntityState.Modified;
                }
            }
        }
    }
}

public class EditReportCommandValidator : AbstractValidator<EditReportCommand>
{
    readonly ApplicationContext _context;

    public EditReportCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Report with id {PropertyValue} does not exists");
        RuleFor(x => x.ReportName).MustAsync(async (request, reportName, cancellation) => await _context.NotExists<ReportState>(x => x.ReportName == reportName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Report with reportName {PropertyValue} already exists");
	
    }
}
