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

namespace CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;

public record AddReportQueryFilterCommand : ReportQueryFilterState, IRequest<Validation<Error, ReportQueryFilterState>>;

public class AddReportQueryFilterCommandHandler : BaseCommandHandler<ApplicationContext, ReportQueryFilterState, AddReportQueryFilterCommand>, IRequestHandler<AddReportQueryFilterCommand, Validation<Error, ReportQueryFilterState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportQueryFilterCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportQueryFilterCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportQueryFilterState>> Handle(AddReportQueryFilterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportQueryFilterCommandValidator : AbstractValidator<AddReportQueryFilterCommand>
{
    readonly ApplicationContext _context;

    public AddReportQueryFilterCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportQueryFilterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportQueryFilter with id {PropertyValue} already exists");
        
    }
}
