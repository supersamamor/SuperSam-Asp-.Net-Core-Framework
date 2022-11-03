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

namespace CTI.ELMS.Application.Features.ELMS.UnitOffered.Commands;

public record AddUnitOfferedCommand : UnitOfferedState, IRequest<Validation<Error, UnitOfferedState>>;

public class AddUnitOfferedCommandHandler : BaseCommandHandler<ApplicationContext, UnitOfferedState, AddUnitOfferedCommand>, IRequestHandler<AddUnitOfferedCommand, Validation<Error, UnitOfferedState>>
{
	private readonly IdentityContext _identityContext;
    public AddUnitOfferedCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUnitOfferedCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, UnitOfferedState>> Handle(AddUnitOfferedCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddUnitOffered(request, cancellationToken));


	public async Task<Validation<Error, UnitOfferedState>> AddUnitOffered(AddUnitOfferedCommand request, CancellationToken cancellationToken)
	{
		UnitOfferedState entity = Mapper.Map<UnitOfferedState>(request);
		UpdateAnnualIncrementList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, UnitOfferedState>(entity);
	}
	
	private void UpdateAnnualIncrementList(UnitOfferedState entity)
	{
		if (entity.AnnualIncrementList?.Count > 0)
		{
			foreach (var annualIncrement in entity.AnnualIncrementList!)
			{
				Context.Entry(annualIncrement).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddUnitOfferedCommandValidator : AbstractValidator<AddUnitOfferedCommand>
{
    readonly ApplicationContext _context;

    public AddUnitOfferedCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UnitOfferedState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitOffered with id {PropertyValue} already exists");
        
    }
}
