using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Commands;

public record AddScheduleFrequencyParameterSetupCommand : ScheduleFrequencyParameterSetupState, IRequest<Validation<Error, ScheduleFrequencyParameterSetupState>>;

public class AddScheduleFrequencyParameterSetupCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleFrequencyParameterSetupState, AddScheduleFrequencyParameterSetupCommand>, IRequestHandler<AddScheduleFrequencyParameterSetupCommand, Validation<Error, ScheduleFrequencyParameterSetupState>>
{
	private readonly IdentityContext _identityContext;
    public AddScheduleFrequencyParameterSetupCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddScheduleFrequencyParameterSetupCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ScheduleFrequencyParameterSetupState>> Handle(AddScheduleFrequencyParameterSetupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddScheduleFrequencyParameterSetupCommandValidator : AbstractValidator<AddScheduleFrequencyParameterSetupCommand>
{
    readonly ApplicationContext _context;

    public AddScheduleFrequencyParameterSetupCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ScheduleFrequencyParameterSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleFrequencyParameterSetup with id {PropertyValue} already exists");
        
    }
}
