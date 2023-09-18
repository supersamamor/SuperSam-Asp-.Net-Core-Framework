using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Assignment.Commands;

public record EditAssignmentCommand : AssignmentState, IRequest<Validation<Error, AssignmentState>>;

public class EditAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, AssignmentState, EditAssignmentCommand>, IRequestHandler<EditAssignmentCommand, Validation<Error, AssignmentState>>
{
    public EditAssignmentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditAssignmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, AssignmentState>> Handle(EditAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditAssignment(request, cancellationToken));


	public async Task<Validation<Error, AssignmentState>> EditAssignment(EditAssignmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Assignment.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateDeliveryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, AssignmentState>(entity);
	}
	
	private async Task UpdateDeliveryList(AssignmentState entity, EditAssignmentCommand request, CancellationToken cancellationToken)
	{
		IList<DeliveryState> deliveryListForDeletion = new List<DeliveryState>();
		var queryDeliveryForDeletion = Context.Delivery.Where(l => l.AssignmentCode == request.Id).AsNoTracking();
		if (entity.DeliveryList?.Count > 0)
		{
			queryDeliveryForDeletion = queryDeliveryForDeletion.Where(l => !(entity.DeliveryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		deliveryListForDeletion = await queryDeliveryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var delivery in deliveryListForDeletion!)
		{
			Context.Entry(delivery).State = EntityState.Deleted;
		}
		if (entity.DeliveryList?.Count > 0)
		{
			foreach (var delivery in entity.DeliveryList.Where(l => !deliveryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<DeliveryState>(x => x.Id == delivery.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(delivery).State = EntityState.Added;
				}
				else
				{
					Context.Entry(delivery).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditAssignmentCommandValidator : AbstractValidator<EditAssignmentCommand>
{
    readonly ApplicationContext _context;

    public EditAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<AssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Assignment with id {PropertyValue} does not exists");
        RuleFor(x => x.AssignmentCode).MustAsync(async (request, assignmentCode, cancellation) => await _context.NotExists<AssignmentState>(x => x.AssignmentCode == assignmentCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Assignment with assignmentCode {PropertyValue} already exists");
	
    }
}
