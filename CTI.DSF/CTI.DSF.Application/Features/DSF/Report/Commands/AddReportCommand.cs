using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Report.Commands;

public record AddReportCommand : ReportState, IRequest<Validation<Error, ReportState>>;

public class AddReportCommandHandler : BaseCommandHandler<ApplicationContext, ReportState, AddReportCommand>, IRequestHandler<AddReportCommand, Validation<Error, ReportState>>
{

    public AddReportCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportCommand> validator) : base(context, mapper, validator)
    {
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
		UpdateReportRoleAssignmentList(entity);
        _ = await Context.AddAsync(entity, cancellationToken);	
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
    private void UpdateReportRoleAssignmentList(ReportState entity)
    {
        if (entity.ReportQueryFilterList?.Count > 0)
        {
            foreach (var reportRoleAssignment in entity.ReportRoleAssignmentList!)
            {
                Context.Entry(reportRoleAssignment).State = EntityState.Added;
            }
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
