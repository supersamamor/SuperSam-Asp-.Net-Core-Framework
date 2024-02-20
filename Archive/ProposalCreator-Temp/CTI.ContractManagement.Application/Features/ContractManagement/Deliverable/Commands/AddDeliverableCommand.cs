using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Commands;

public record AddDeliverableCommand : DeliverableState, IRequest<Validation<Error, DeliverableState>>;

public class AddDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, DeliverableState, AddDeliverableCommand>, IRequestHandler<AddDeliverableCommand, Validation<Error, DeliverableState>>
{
	private readonly IdentityContext _identityContext;
    public AddDeliverableCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddDeliverableCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, DeliverableState>> Handle(AddDeliverableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddDeliverable(request, cancellationToken));


	public async Task<Validation<Error, DeliverableState>> AddDeliverable(AddDeliverableCommand request, CancellationToken cancellationToken)
	{
		DeliverableState entity = Mapper.Map<DeliverableState>(request);
		UpdateProjectDeliverableList(entity);
		UpdateProjectPackageAdditionalDeliverableList(entity);
		UpdateProjectDeliverableHistoryList(entity);
		UpdateProjectPackageAdditionalDeliverableHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DeliverableState>(entity);
	}
	
	private void UpdateProjectDeliverableList(DeliverableState entity)
	{
		if (entity.ProjectDeliverableList?.Count > 0)
		{
			foreach (var projectDeliverable in entity.ProjectDeliverableList!)
			{
				Context.Entry(projectDeliverable).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectPackageAdditionalDeliverableList(DeliverableState entity)
	{
		if (entity.ProjectPackageAdditionalDeliverableList?.Count > 0)
		{
			foreach (var projectPackageAdditionalDeliverable in entity.ProjectPackageAdditionalDeliverableList!)
			{
				Context.Entry(projectPackageAdditionalDeliverable).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectDeliverableHistoryList(DeliverableState entity)
	{
		if (entity.ProjectDeliverableHistoryList?.Count > 0)
		{
			foreach (var projectDeliverableHistory in entity.ProjectDeliverableHistoryList!)
			{
				Context.Entry(projectDeliverableHistory).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectPackageAdditionalDeliverableHistoryList(DeliverableState entity)
	{
		if (entity.ProjectPackageAdditionalDeliverableHistoryList?.Count > 0)
		{
			foreach (var projectPackageAdditionalDeliverableHistory in entity.ProjectPackageAdditionalDeliverableHistoryList!)
			{
				Context.Entry(projectPackageAdditionalDeliverableHistory).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddDeliverableCommandValidator : AbstractValidator<AddDeliverableCommand>
{
    readonly ApplicationContext _context;

    public AddDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<DeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Deliverable with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<DeliverableState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Deliverable with name {PropertyValue} already exists");
	
    }
}
