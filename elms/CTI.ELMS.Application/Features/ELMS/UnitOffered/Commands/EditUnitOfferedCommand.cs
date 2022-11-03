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

public record EditUnitOfferedCommand : UnitOfferedState, IRequest<Validation<Error, UnitOfferedState>>;

public class EditUnitOfferedCommandHandler : BaseCommandHandler<ApplicationContext, UnitOfferedState, EditUnitOfferedCommand>, IRequestHandler<EditUnitOfferedCommand, Validation<Error, UnitOfferedState>>
{
    public EditUnitOfferedCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUnitOfferedCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitOfferedState>> Handle(EditUnitOfferedCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditUnitOffered(request, cancellationToken));


	public async Task<Validation<Error, UnitOfferedState>> EditUnitOffered(EditUnitOfferedCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.UnitOffered.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateAnnualIncrementList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, UnitOfferedState>(entity);
	}
	
	private async Task UpdateAnnualIncrementList(UnitOfferedState entity, EditUnitOfferedCommand request, CancellationToken cancellationToken)
	{
		IList<AnnualIncrementState> annualIncrementListForDeletion = new List<AnnualIncrementState>();
		var queryAnnualIncrementForDeletion = Context.AnnualIncrement.Where(l => l.UnitOfferedID == request.Id).AsNoTracking();
		if (entity.AnnualIncrementList?.Count > 0)
		{
			queryAnnualIncrementForDeletion = queryAnnualIncrementForDeletion.Where(l => !(entity.AnnualIncrementList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		annualIncrementListForDeletion = await queryAnnualIncrementForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var annualIncrement in annualIncrementListForDeletion!)
		{
			Context.Entry(annualIncrement).State = EntityState.Deleted;
		}
		if (entity.AnnualIncrementList?.Count > 0)
		{
			foreach (var annualIncrement in entity.AnnualIncrementList.Where(l => !annualIncrementListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<AnnualIncrementState>(x => x.Id == annualIncrement.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(annualIncrement).State = EntityState.Added;
				}
				else
				{
					Context.Entry(annualIncrement).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditUnitOfferedCommandValidator : AbstractValidator<EditUnitOfferedCommand>
{
    readonly ApplicationContext _context;

    public EditUnitOfferedCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitOfferedState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitOffered with id {PropertyValue} does not exists");
        
    }
}
