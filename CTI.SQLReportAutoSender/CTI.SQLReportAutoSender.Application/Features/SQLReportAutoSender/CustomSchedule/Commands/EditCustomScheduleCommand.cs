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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Commands;

public record EditCustomScheduleCommand : CustomScheduleState, IRequest<Validation<Error, CustomScheduleState>>;

public class EditCustomScheduleCommandHandler : BaseCommandHandler<ApplicationContext, CustomScheduleState, EditCustomScheduleCommand>, IRequestHandler<EditCustomScheduleCommand, Validation<Error, CustomScheduleState>>
{
    public EditCustomScheduleCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCustomScheduleCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CustomScheduleState>> Handle(EditCustomScheduleCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCustomScheduleCommandValidator : AbstractValidator<EditCustomScheduleCommand>
{
    readonly ApplicationContext _context;

    public EditCustomScheduleCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CustomScheduleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CustomSchedule with id {PropertyValue} does not exists");
        
    }
}
