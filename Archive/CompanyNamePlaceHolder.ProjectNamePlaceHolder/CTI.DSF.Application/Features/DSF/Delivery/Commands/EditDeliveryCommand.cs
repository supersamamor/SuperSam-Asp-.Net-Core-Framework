using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Delivery.Commands;

public record EditDeliveryCommand : DeliveryState, IRequest<Validation<Error, DeliveryState>>;

public class EditDeliveryCommandHandler : BaseCommandHandler<ApplicationContext, DeliveryState, EditDeliveryCommand>, IRequestHandler<EditDeliveryCommand, Validation<Error, DeliveryState>>
{
    public EditDeliveryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDeliveryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DeliveryState>> Handle(EditDeliveryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditDelivery(request, cancellationToken));


	public async Task<Validation<Error, DeliveryState>> EditDelivery(EditDeliveryCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Delivery.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateDeliveryApprovalHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DeliveryState>(entity);
	}
	
	private async Task UpdateDeliveryApprovalHistoryList(DeliveryState entity, EditDeliveryCommand request, CancellationToken cancellationToken)
	{
		IList<DeliveryApprovalHistoryState> deliveryApprovalHistoryListForDeletion = new List<DeliveryApprovalHistoryState>();
		var queryDeliveryApprovalHistoryForDeletion = Context.DeliveryApprovalHistory.Where(l => l.DeliveryId == request.Id).AsNoTracking();
		if (entity.DeliveryApprovalHistoryList?.Count > 0)
		{
			queryDeliveryApprovalHistoryForDeletion = queryDeliveryApprovalHistoryForDeletion.Where(l => !(entity.DeliveryApprovalHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		deliveryApprovalHistoryListForDeletion = await queryDeliveryApprovalHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var deliveryApprovalHistory in deliveryApprovalHistoryListForDeletion!)
		{
			Context.Entry(deliveryApprovalHistory).State = EntityState.Deleted;
		}
		if (entity.DeliveryApprovalHistoryList?.Count > 0)
		{
			foreach (var deliveryApprovalHistory in entity.DeliveryApprovalHistoryList.Where(l => !deliveryApprovalHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<DeliveryApprovalHistoryState>(x => x.Id == deliveryApprovalHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(deliveryApprovalHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(deliveryApprovalHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditDeliveryCommandValidator : AbstractValidator<EditDeliveryCommand>
{
    readonly ApplicationContext _context;

    public EditDeliveryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DeliveryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Delivery with id {PropertyValue} does not exists");
        
    }
}
