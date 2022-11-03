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

namespace CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Commands;

public record AddUnitOfferedHistoryCommand : UnitOfferedHistoryState, IRequest<Validation<Error, UnitOfferedHistoryState>>;

public class AddUnitOfferedHistoryCommandHandler : BaseCommandHandler<ApplicationContext, UnitOfferedHistoryState, AddUnitOfferedHistoryCommand>, IRequestHandler<AddUnitOfferedHistoryCommand, Validation<Error, UnitOfferedHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddUnitOfferedHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUnitOfferedHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, UnitOfferedHistoryState>> Handle(AddUnitOfferedHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddUnitOfferedHistory(request, cancellationToken));


	public async Task<Validation<Error, UnitOfferedHistoryState>> AddUnitOfferedHistory(AddUnitOfferedHistoryCommand request, CancellationToken cancellationToken)
	{
		UnitOfferedHistoryState entity = Mapper.Map<UnitOfferedHistoryState>(request);
		UpdateAnnualIncrementHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, UnitOfferedHistoryState>(entity);
	}
	
	private void UpdateAnnualIncrementHistoryList(UnitOfferedHistoryState entity)
	{
		if (entity.AnnualIncrementHistoryList?.Count > 0)
		{
			foreach (var annualIncrementHistory in entity.AnnualIncrementHistoryList!)
			{
				Context.Entry(annualIncrementHistory).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddUnitOfferedHistoryCommandValidator : AbstractValidator<AddUnitOfferedHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddUnitOfferedHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UnitOfferedHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitOfferedHistory with id {PropertyValue} already exists");
        
    }
}
