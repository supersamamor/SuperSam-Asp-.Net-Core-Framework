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

public record AddCustomScheduleCommand : CustomScheduleState, IRequest<Validation<Error, CustomScheduleState>>;

public class AddCustomScheduleCommandHandler : BaseCommandHandler<ApplicationContext, CustomScheduleState, AddCustomScheduleCommand>, IRequestHandler<AddCustomScheduleCommand, Validation<Error, CustomScheduleState>>
{
	private readonly IdentityContext _identityContext;
    public AddCustomScheduleCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCustomScheduleCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, CustomScheduleState>> Handle(AddCustomScheduleCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddCustomScheduleCommandValidator : AbstractValidator<AddCustomScheduleCommand>
{
    readonly ApplicationContext _context;

    public AddCustomScheduleCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CustomScheduleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CustomSchedule with id {PropertyValue} already exists");
        
    }
}
