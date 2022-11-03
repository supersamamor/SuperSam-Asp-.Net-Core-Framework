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

public record EditUnitOfferedHistoryCommand : UnitOfferedHistoryState, IRequest<Validation<Error, UnitOfferedHistoryState>>;

public class EditUnitOfferedHistoryCommandHandler : BaseCommandHandler<ApplicationContext, UnitOfferedHistoryState, EditUnitOfferedHistoryCommand>, IRequestHandler<EditUnitOfferedHistoryCommand, Validation<Error, UnitOfferedHistoryState>>
{
    public EditUnitOfferedHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUnitOfferedHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitOfferedHistoryState>> Handle(EditUnitOfferedHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditUnitOfferedHistory(request, cancellationToken));


	public async Task<Validation<Error, UnitOfferedHistoryState>> EditUnitOfferedHistory(EditUnitOfferedHistoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.UnitOfferedHistory.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateAnnualIncrementHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, UnitOfferedHistoryState>(entity);
	}
	
	private async Task UpdateAnnualIncrementHistoryList(UnitOfferedHistoryState entity, EditUnitOfferedHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<AnnualIncrementHistoryState> annualIncrementHistoryListForDeletion = new List<AnnualIncrementHistoryState>();
		var queryAnnualIncrementHistoryForDeletion = Context.AnnualIncrementHistory.Where(l => l.UnitOfferedHistoryID == request.Id).AsNoTracking();
		if (entity.AnnualIncrementHistoryList?.Count > 0)
		{
			queryAnnualIncrementHistoryForDeletion = queryAnnualIncrementHistoryForDeletion.Where(l => !(entity.AnnualIncrementHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		annualIncrementHistoryListForDeletion = await queryAnnualIncrementHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var annualIncrementHistory in annualIncrementHistoryListForDeletion!)
		{
			Context.Entry(annualIncrementHistory).State = EntityState.Deleted;
		}
		if (entity.AnnualIncrementHistoryList?.Count > 0)
		{
			foreach (var annualIncrementHistory in entity.AnnualIncrementHistoryList.Where(l => !annualIncrementHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<AnnualIncrementHistoryState>(x => x.Id == annualIncrementHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(annualIncrementHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(annualIncrementHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditUnitOfferedHistoryCommandValidator : AbstractValidator<EditUnitOfferedHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditUnitOfferedHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitOfferedHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitOfferedHistory with id {PropertyValue} does not exists");
        
    }
}
