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

namespace CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;

public record AddReportColumnFilterCommand : ReportColumnFilterState, IRequest<Validation<Error, ReportColumnFilterState>>;

public class AddReportColumnFilterCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnFilterState, AddReportColumnFilterCommand>, IRequestHandler<AddReportColumnFilterCommand, Validation<Error, ReportColumnFilterState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportColumnFilterCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportColumnFilterCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportColumnFilterState>> Handle(AddReportColumnFilterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportColumnFilterCommandValidator : AbstractValidator<AddReportColumnFilterCommand>
{
    readonly ApplicationContext _context;

    public AddReportColumnFilterCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportColumnFilterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnFilter with id {PropertyValue} already exists");
        
    }
}
