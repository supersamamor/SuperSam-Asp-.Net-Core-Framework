using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Commands;

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
		UpdateReportQueryFilterList(entity);
		UpdateReportRoleAssignmentList(entity);
        _ = await Context.AddAsync(entity, cancellationToken);	
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportState>(entity);
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
