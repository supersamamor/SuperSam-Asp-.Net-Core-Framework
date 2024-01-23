using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Commands;

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
		await UpdateReportQueryFilterList(entity, request, cancellationToken);
        await UpdateReportRoleAssignmentList(entity, request, cancellationToken);
        Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportState>(entity);
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
