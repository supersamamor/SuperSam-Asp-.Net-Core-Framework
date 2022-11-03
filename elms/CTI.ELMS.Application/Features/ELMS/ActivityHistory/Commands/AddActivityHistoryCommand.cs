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

namespace CTI.ELMS.Application.Features.ELMS.ActivityHistory.Commands;

public record AddActivityHistoryCommand : ActivityHistoryState, IRequest<Validation<Error, ActivityHistoryState>>;

public class AddActivityHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ActivityHistoryState, AddActivityHistoryCommand>, IRequestHandler<AddActivityHistoryCommand, Validation<Error, ActivityHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddActivityHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddActivityHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ActivityHistoryState>> Handle(AddActivityHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddActivityHistory(request, cancellationToken));


	public async Task<Validation<Error, ActivityHistoryState>> AddActivityHistory(AddActivityHistoryCommand request, CancellationToken cancellationToken)
	{
		ActivityHistoryState entity = Mapper.Map<ActivityHistoryState>(request);
		UpdateUnitActivityList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ActivityHistoryState>(entity);
	}
	
	private void UpdateUnitActivityList(ActivityHistoryState entity)
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

public class AddActivityHistoryCommandValidator : AbstractValidator<AddActivityHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddActivityHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ActivityHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ActivityHistory with id {PropertyValue} already exists");
        
    }
}
