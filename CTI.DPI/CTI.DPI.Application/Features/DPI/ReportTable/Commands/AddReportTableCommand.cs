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

namespace CTI.DPI.Application.Features.DPI.ReportTable.Commands;

public record AddReportTableCommand : ReportTableState, IRequest<Validation<Error, ReportTableState>>;

public class AddReportTableCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableState, AddReportTableCommand>, IRequestHandler<AddReportTableCommand, Validation<Error, ReportTableState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportTableCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportTableCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ReportTableState>> Handle(AddReportTableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddReportTable(request, cancellationToken));


	public async Task<Validation<Error, ReportTableState>> AddReportTable(AddReportTableCommand request, CancellationToken cancellationToken)
	{
		ReportTableState entity = Mapper.Map<ReportTableState>(request);
		UpdateReportTableJoinParameterList(entity);
		UpdateReportColumnDetailList(entity);
		UpdateReportColumnFilterList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportTableState>(entity);
	}
	
	private void UpdateReportTableJoinParameterList(ReportTableState entity)
	{
		if (entity.ReportTableJoinParameterList?.Count > 0)
		{
			foreach (var reportTableJoinParameter in entity.ReportTableJoinParameterList!)
			{
				Context.Entry(reportTableJoinParameter).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportColumnDetailList(ReportTableState entity)
	{
		if (entity.ReportColumnDetailList?.Count > 0)
		{
			foreach (var reportColumnDetail in entity.ReportColumnDetailList!)
			{
				Context.Entry(reportColumnDetail).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportColumnFilterList(ReportTableState entity)
	{
		if (entity.ReportColumnFilterList?.Count > 0)
		{
			foreach (var reportColumnFilter in entity.ReportColumnFilterList!)
			{
				Context.Entry(reportColumnFilter).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddReportTableCommandValidator : AbstractValidator<AddReportTableCommand>
{
    readonly ApplicationContext _context;

    public AddReportTableCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportTableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTable with id {PropertyValue} already exists");
        
    }
}
