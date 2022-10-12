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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Commands;

public record AddScheduleFrequencyCommand : ScheduleFrequencyState, IRequest<Validation<Error, ScheduleFrequencyState>>;

public class AddScheduleFrequencyCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleFrequencyState, AddScheduleFrequencyCommand>, IRequestHandler<AddScheduleFrequencyCommand, Validation<Error, ScheduleFrequencyState>>
{
	private readonly IdentityContext _identityContext;
    public AddScheduleFrequencyCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddScheduleFrequencyCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ScheduleFrequencyState>> Handle(AddScheduleFrequencyCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddScheduleFrequency(request, cancellationToken));


	public async Task<Validation<Error, ScheduleFrequencyState>> AddScheduleFrequency(AddScheduleFrequencyCommand request, CancellationToken cancellationToken)
	{
		ScheduleFrequencyState entity = Mapper.Map<ScheduleFrequencyState>(request);
		UpdateScheduleFrequencyParameterSetupList(entity);	
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ScheduleFrequencyState>(entity);
	}
	
	private void UpdateScheduleFrequencyParameterSetupList(ScheduleFrequencyState entity)
	{
		if (entity.ScheduleFrequencyParameterSetupList?.Count > 0)
		{
			foreach (var scheduleFrequencyParameterSetup in entity.ScheduleFrequencyParameterSetupList!)
			{
				Context.Entry(scheduleFrequencyParameterSetup).State = EntityState.Added;
			}
		}
	}
}

public class AddScheduleFrequencyCommandValidator : AbstractValidator<AddScheduleFrequencyCommand>
{
    readonly ApplicationContext _context;

    public AddScheduleFrequencyCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ScheduleFrequencyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleFrequency with id {PropertyValue} already exists");
        RuleFor(x => x.Description).MustAsync(async (description, cancellation) => await _context.NotExists<ScheduleFrequencyState>(x => x.Description == description, cancellationToken: cancellation)).WithMessage("ScheduleFrequency with description {PropertyValue} already exists");
	
    }
}
