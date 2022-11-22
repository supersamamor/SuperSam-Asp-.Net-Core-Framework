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

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record EditOfferingCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class EditOfferingCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, EditOfferingCommand>, IRequestHandler<EditOfferingCommand, Validation<Error, OfferingState>>
{
    public EditOfferingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditOfferingCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OfferingState>> Handle(EditOfferingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditOffering(request, cancellationToken));


	public async Task<Validation<Error, OfferingState>> EditOffering(EditOfferingCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Offering.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);	
		await UpdatePreSelectedUnitList(entity, request, cancellationToken);
		await UpdateUnitOfferedList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OfferingState>(entity);
	}
	
	
	private async Task UpdatePreSelectedUnitList(OfferingState entity, EditOfferingCommand request, CancellationToken cancellationToken)
	{
		IList<PreSelectedUnitState> preSelectedUnitListForDeletion = new List<PreSelectedUnitState>();
		var queryPreSelectedUnitForDeletion = Context.PreSelectedUnit.Where(l => l.OfferingID == request.Id).AsNoTracking();
		if (entity.PreSelectedUnitList?.Count > 0)
		{
			queryPreSelectedUnitForDeletion = queryPreSelectedUnitForDeletion.Where(l => !(entity.PreSelectedUnitList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		preSelectedUnitListForDeletion = await queryPreSelectedUnitForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var preSelectedUnit in preSelectedUnitListForDeletion!)
		{
			Context.Entry(preSelectedUnit).State = EntityState.Deleted;
		}
		if (entity.PreSelectedUnitList?.Count > 0)
		{
			foreach (var preSelectedUnit in entity.PreSelectedUnitList.Where(l => !preSelectedUnitListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<PreSelectedUnitState>(x => x.Id == preSelectedUnit.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(preSelectedUnit).State = EntityState.Added;
				}
				else
				{
					Context.Entry(preSelectedUnit).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateUnitOfferedList(OfferingState entity, EditOfferingCommand request, CancellationToken cancellationToken)
	{
		IList<UnitOfferedState> unitOfferedListForDeletion = new List<UnitOfferedState>();
		var queryUnitOfferedForDeletion = Context.UnitOffered.Where(l => l.OfferingID == request.Id).AsNoTracking();
		if (entity.UnitOfferedList?.Count > 0)
		{
			queryUnitOfferedForDeletion = queryUnitOfferedForDeletion.Where(l => !(entity.UnitOfferedList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		unitOfferedListForDeletion = await queryUnitOfferedForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var unitOffered in unitOfferedListForDeletion!)
		{
			Context.Entry(unitOffered).State = EntityState.Deleted;
		}
		if (entity.UnitOfferedList?.Count > 0)
		{
			foreach (var unitOffered in entity.UnitOfferedList.Where(l => !unitOfferedListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<UnitOfferedState>(x => x.Id == unitOffered.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(unitOffered).State = EntityState.Added;
				}
				else
				{
					Context.Entry(unitOffered).State = EntityState.Modified;
				}
			}
		}
	}
	
	
}

public class EditOfferingCommandValidator : AbstractValidator<EditOfferingCommand>
{
    readonly ApplicationContext _context;

    public EditOfferingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} does not exists");
        
    }
}
