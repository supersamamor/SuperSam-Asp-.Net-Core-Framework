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

public record EditDeliverableCommand : DeliverableState, IRequest<Validation<Error, DeliverableState>>;

public class EditDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, DeliverableState, EditDeliverableCommand>, IRequestHandler<EditDeliverableCommand, Validation<Error, DeliverableState>>
{
    public EditDeliverableCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDeliverableCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DeliverableState>> Handle(EditDeliverableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditDeliverable(request, cancellationToken));


	public async Task<Validation<Error, DeliverableState>> EditDeliverable(EditDeliverableCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Deliverable.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProjectDeliverableList(entity, request, cancellationToken);
		await UpdateProjectPackageAdditionalDeliverableList(entity, request, cancellationToken);
		await UpdateProjectDeliverableHistoryList(entity, request, cancellationToken);
		await UpdateProjectPackageAdditionalDeliverableHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DeliverableState>(entity);
	}
	
	private async Task UpdateProjectDeliverableList(DeliverableState entity, EditDeliverableCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectDeliverableState> projectDeliverableListForDeletion = new List<ProjectDeliverableState>();
		var queryProjectDeliverableForDeletion = Context.ProjectDeliverable.Where(l => l.DeliverableId == request.Id).AsNoTracking();
		if (entity.ProjectDeliverableList?.Count > 0)
		{
			queryProjectDeliverableForDeletion = queryProjectDeliverableForDeletion.Where(l => !(entity.ProjectDeliverableList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectDeliverableListForDeletion = await queryProjectDeliverableForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectDeliverable in projectDeliverableListForDeletion!)
		{
			Context.Entry(projectDeliverable).State = EntityState.Deleted;
		}
		if (entity.ProjectDeliverableList?.Count > 0)
		{
			foreach (var projectDeliverable in entity.ProjectDeliverableList.Where(l => !projectDeliverableListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectDeliverableState>(x => x.Id == projectDeliverable.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectDeliverable).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectDeliverable).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateProjectPackageAdditionalDeliverableList(DeliverableState entity, EditDeliverableCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectPackageAdditionalDeliverableState> projectPackageAdditionalDeliverableListForDeletion = new List<ProjectPackageAdditionalDeliverableState>();
		var queryProjectPackageAdditionalDeliverableForDeletion = Context.ProjectPackageAdditionalDeliverable.Where(l => l.DeliverableId == request.Id).AsNoTracking();
		if (entity.ProjectPackageAdditionalDeliverableList?.Count > 0)
		{
			queryProjectPackageAdditionalDeliverableForDeletion = queryProjectPackageAdditionalDeliverableForDeletion.Where(l => !(entity.ProjectPackageAdditionalDeliverableList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectPackageAdditionalDeliverableListForDeletion = await queryProjectPackageAdditionalDeliverableForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectPackageAdditionalDeliverable in projectPackageAdditionalDeliverableListForDeletion!)
		{
			Context.Entry(projectPackageAdditionalDeliverable).State = EntityState.Deleted;
		}
		if (entity.ProjectPackageAdditionalDeliverableList?.Count > 0)
		{
			foreach (var projectPackageAdditionalDeliverable in entity.ProjectPackageAdditionalDeliverableList.Where(l => !projectPackageAdditionalDeliverableListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectPackageAdditionalDeliverableState>(x => x.Id == projectPackageAdditionalDeliverable.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectPackageAdditionalDeliverable).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectPackageAdditionalDeliverable).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateProjectDeliverableHistoryList(DeliverableState entity, EditDeliverableCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectDeliverableHistoryState> projectDeliverableHistoryListForDeletion = new List<ProjectDeliverableHistoryState>();
		var queryProjectDeliverableHistoryForDeletion = Context.ProjectDeliverableHistory.Where(l => l.DeliverableId == request.Id).AsNoTracking();
		if (entity.ProjectDeliverableHistoryList?.Count > 0)
		{
			queryProjectDeliverableHistoryForDeletion = queryProjectDeliverableHistoryForDeletion.Where(l => !(entity.ProjectDeliverableHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectDeliverableHistoryListForDeletion = await queryProjectDeliverableHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectDeliverableHistory in projectDeliverableHistoryListForDeletion!)
		{
			Context.Entry(projectDeliverableHistory).State = EntityState.Deleted;
		}
		if (entity.ProjectDeliverableHistoryList?.Count > 0)
		{
			foreach (var projectDeliverableHistory in entity.ProjectDeliverableHistoryList.Where(l => !projectDeliverableHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectDeliverableHistoryState>(x => x.Id == projectDeliverableHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectDeliverableHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectDeliverableHistory).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateProjectPackageAdditionalDeliverableHistoryList(DeliverableState entity, EditDeliverableCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectPackageAdditionalDeliverableHistoryState> projectPackageAdditionalDeliverableHistoryListForDeletion = new List<ProjectPackageAdditionalDeliverableHistoryState>();
		var queryProjectPackageAdditionalDeliverableHistoryForDeletion = Context.ProjectPackageAdditionalDeliverableHistory.Where(l => l.DeliverableId == request.Id).AsNoTracking();
		if (entity.ProjectPackageAdditionalDeliverableHistoryList?.Count > 0)
		{
			queryProjectPackageAdditionalDeliverableHistoryForDeletion = queryProjectPackageAdditionalDeliverableHistoryForDeletion.Where(l => !(entity.ProjectPackageAdditionalDeliverableHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectPackageAdditionalDeliverableHistoryListForDeletion = await queryProjectPackageAdditionalDeliverableHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectPackageAdditionalDeliverableHistory in projectPackageAdditionalDeliverableHistoryListForDeletion!)
		{
			Context.Entry(projectPackageAdditionalDeliverableHistory).State = EntityState.Deleted;
		}
		if (entity.ProjectPackageAdditionalDeliverableHistoryList?.Count > 0)
		{
			foreach (var projectPackageAdditionalDeliverableHistory in entity.ProjectPackageAdditionalDeliverableHistoryList.Where(l => !projectPackageAdditionalDeliverableHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectPackageAdditionalDeliverableHistoryState>(x => x.Id == projectPackageAdditionalDeliverableHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectPackageAdditionalDeliverableHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectPackageAdditionalDeliverableHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditDeliverableCommandValidator : AbstractValidator<EditDeliverableCommand>
{
    readonly ApplicationContext _context;

    public EditDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Deliverable with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<DeliverableState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Deliverable with name {PropertyValue} already exists");
	
    }
}
