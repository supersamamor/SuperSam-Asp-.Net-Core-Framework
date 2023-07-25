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

namespace CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Commands;

public record AddReportTableJoinParameterCommand : ReportTableJoinParameterState, IRequest<Validation<Error, ReportTableJoinParameterState>>;

public class AddReportTableJoinParameterCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableJoinParameterState, AddReportTableJoinParameterCommand>, IRequestHandler<AddReportTableJoinParameterCommand, Validation<Error, ReportTableJoinParameterState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportTableJoinParameterCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportTableJoinParameterCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportTableJoinParameterState>> Handle(AddReportTableJoinParameterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportTableJoinParameterCommandValidator : AbstractValidator<AddReportTableJoinParameterCommand>
{
    readonly ApplicationContext _context;

    public AddReportTableJoinParameterCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportTableJoinParameterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableJoinParameter with id {PropertyValue} already exists");
        
    }
}
