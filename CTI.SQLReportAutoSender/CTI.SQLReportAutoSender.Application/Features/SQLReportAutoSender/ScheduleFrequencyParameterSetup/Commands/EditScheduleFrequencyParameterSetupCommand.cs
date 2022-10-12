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

public record EditScheduleFrequencyParameterSetupCommand : ScheduleFrequencyParameterSetupState, IRequest<Validation<Error, ScheduleFrequencyParameterSetupState>>;

public class EditScheduleFrequencyParameterSetupCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleFrequencyParameterSetupState, EditScheduleFrequencyParameterSetupCommand>, IRequestHandler<EditScheduleFrequencyParameterSetupCommand, Validation<Error, ScheduleFrequencyParameterSetupState>>
{
    public EditScheduleFrequencyParameterSetupCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditScheduleFrequencyParameterSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ScheduleFrequencyParameterSetupState>> Handle(EditScheduleFrequencyParameterSetupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditScheduleFrequencyParameterSetupCommandValidator : AbstractValidator<EditScheduleFrequencyParameterSetupCommand>
{
    readonly ApplicationContext _context;

    public EditScheduleFrequencyParameterSetupCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ScheduleFrequencyParameterSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleFrequencyParameterSetup with id {PropertyValue} does not exists");
        
    }
}
