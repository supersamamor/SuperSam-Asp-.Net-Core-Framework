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

namespace CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;

public record AddReportColumnDetailCommand : ReportColumnDetailState, IRequest<Validation<Error, ReportColumnDetailState>>;

public class AddReportColumnDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnDetailState, AddReportColumnDetailCommand>, IRequestHandler<AddReportColumnDetailCommand, Validation<Error, ReportColumnDetailState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportColumnDetailCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportColumnDetailCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportColumnDetailState>> Handle(AddReportColumnDetailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportColumnDetailCommandValidator : AbstractValidator<AddReportColumnDetailCommand>
{
    readonly ApplicationContext _context;

    public AddReportColumnDetailCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportColumnDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnDetail with id {PropertyValue} already exists");
        
    }
}
