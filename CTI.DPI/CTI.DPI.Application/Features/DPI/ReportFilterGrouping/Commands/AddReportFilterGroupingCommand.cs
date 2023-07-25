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

namespace CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;

public record AddReportFilterGroupingCommand : ReportFilterGroupingState, IRequest<Validation<Error, ReportFilterGroupingState>>;

public class AddReportFilterGroupingCommandHandler : BaseCommandHandler<ApplicationContext, ReportFilterGroupingState, AddReportFilterGroupingCommand>, IRequestHandler<AddReportFilterGroupingCommand, Validation<Error, ReportFilterGroupingState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportFilterGroupingCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportFilterGroupingCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ReportFilterGroupingState>> Handle(AddReportFilterGroupingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddReportFilterGrouping(request, cancellationToken));


	public async Task<Validation<Error, ReportFilterGroupingState>> AddReportFilterGrouping(AddReportFilterGroupingCommand request, CancellationToken cancellationToken)
	{
		ReportFilterGroupingState entity = Mapper.Map<ReportFilterGroupingState>(request);
		UpdateReportColumnFilterList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportFilterGroupingState>(entity);
	}
	
	private void UpdateReportColumnFilterList(ReportFilterGroupingState entity)
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

public class AddReportFilterGroupingCommandValidator : AbstractValidator<AddReportFilterGroupingCommand>
{
    readonly ApplicationContext _context;

    public AddReportFilterGroupingCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportFilterGroupingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportFilterGrouping with id {PropertyValue} already exists");
        
    }
}
