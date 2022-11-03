using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.Activity.Commands;

public record AddActivityCommand : ActivityState, IRequest<Validation<Error, ActivityState>>;

public class AddActivityCommandHandler : BaseCommandHandler<ApplicationContext, ActivityState, AddActivityCommand>, IRequestHandler<AddActivityCommand, Validation<Error, ActivityState>>
{
	private readonly IdentityContext _identityContext;
    public AddActivityCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddActivityCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ActivityState>> Handle(AddActivityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddActivity(request, cancellationToken));


	public async Task<Validation<Error, ActivityState>> AddActivity(AddActivityCommand request, CancellationToken cancellationToken)
	{
		ActivityState entity = Mapper.Map<ActivityState>(request);
		UpdateActivityHistoryList(entity);
		UpdateUnitActivityList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ActivityState>(entity);
	}
	
	private void UpdateActivityHistoryList(ActivityState entity)
	{
		if (entity.ActivityHistoryList?.Count > 0)
		{
			foreach (var activityHistory in entity.ActivityHistoryList!)
			{
				Context.Entry(activityHistory).State = EntityState.Added;
			}
		}
	}
	private void UpdateUnitActivityList(ActivityState entity)
	{
		if (entity.UnitActivityList?.Count > 0)
		{
			foreach (var unitActivity in entity.UnitActivityList!)
			{
				Context.Entry(unitActivity).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddActivityCommandValidator : AbstractValidator<AddActivityCommand>
{
    readonly ApplicationContext _context;

    public AddActivityCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ActivityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Activity with id {PropertyValue} already exists");
        
    }
}
