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

namespace CTI.ELMS.Application.Features.ELMS.OfferingHistory.Commands;

public record AddOfferingHistoryCommand : OfferingHistoryState, IRequest<Validation<Error, OfferingHistoryState>>;

public class AddOfferingHistoryCommandHandler : BaseCommandHandler<ApplicationContext, OfferingHistoryState, AddOfferingHistoryCommand>, IRequestHandler<AddOfferingHistoryCommand, Validation<Error, OfferingHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddOfferingHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddOfferingHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, OfferingHistoryState>> Handle(AddOfferingHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddOfferingHistory(request, cancellationToken));


	public async Task<Validation<Error, OfferingHistoryState>> AddOfferingHistory(AddOfferingHistoryCommand request, CancellationToken cancellationToken)
	{
		OfferingHistoryState entity = Mapper.Map<OfferingHistoryState>(request);
		UpdateUnitOfferedHistoryList(entity);
		UpdateUnitGroupList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OfferingHistoryState>(entity);
	}
	
	private void UpdateUnitOfferedHistoryList(OfferingHistoryState entity)
	{
		if (entity.UnitOfferedHistoryList?.Count > 0)
		{
			foreach (var unitOfferedHistory in entity.UnitOfferedHistoryList!)
			{
				Context.Entry(unitOfferedHistory).State = EntityState.Added;
			}
		}
	}
	private void UpdateUnitGroupList(OfferingHistoryState entity)
	{
		if (entity.UnitGroupList?.Count > 0)
		{
			foreach (var unitGroup in entity.UnitGroupList!)
			{
				Context.Entry(unitGroup).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddOfferingHistoryCommandValidator : AbstractValidator<AddOfferingHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddOfferingHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<OfferingHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OfferingHistory with id {PropertyValue} already exists");
        
    }
}
