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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Commands;

public record EditProjectHistoryCommand : ProjectHistoryState, IRequest<Validation<Error, ProjectHistoryState>>;

public class EditProjectHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectHistoryState, EditProjectHistoryCommand>, IRequestHandler<EditProjectHistoryCommand, Validation<Error, ProjectHistoryState>>
{
    public EditProjectHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectHistoryState>> Handle(EditProjectHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditProjectHistory(request, cancellationToken));


	public async Task<Validation<Error, ProjectHistoryState>> EditProjectHistory(EditProjectHistoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ProjectHistory.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProjectDeliverableHistoryList(entity, request, cancellationToken);
		await UpdateProjectMilestoneHistoryList(entity, request, cancellationToken);
		await UpdateProjectPackageHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectHistoryState>(entity);
	}
	
	private async Task UpdateProjectDeliverableHistoryList(ProjectHistoryState entity, EditProjectHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectDeliverableHistoryState> projectDeliverableHistoryListForDeletion = new List<ProjectDeliverableHistoryState>();
		var queryProjectDeliverableHistoryForDeletion = Context.ProjectDeliverableHistory.Where(l => l.ProjectHistoryId == request.Id).AsNoTracking();
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
	private async Task UpdateProjectMilestoneHistoryList(ProjectHistoryState entity, EditProjectHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectMilestoneHistoryState> projectMilestoneHistoryListForDeletion = new List<ProjectMilestoneHistoryState>();
		var queryProjectMilestoneHistoryForDeletion = Context.ProjectMilestoneHistory.Where(l => l.ProjectHistoryId == request.Id).AsNoTracking();
		if (entity.ProjectMilestoneHistoryList?.Count > 0)
		{
			queryProjectMilestoneHistoryForDeletion = queryProjectMilestoneHistoryForDeletion.Where(l => !(entity.ProjectMilestoneHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectMilestoneHistoryListForDeletion = await queryProjectMilestoneHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectMilestoneHistory in projectMilestoneHistoryListForDeletion!)
		{
			Context.Entry(projectMilestoneHistory).State = EntityState.Deleted;
		}
		if (entity.ProjectMilestoneHistoryList?.Count > 0)
		{
			foreach (var projectMilestoneHistory in entity.ProjectMilestoneHistoryList.Where(l => !projectMilestoneHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectMilestoneHistoryState>(x => x.Id == projectMilestoneHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectMilestoneHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectMilestoneHistory).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateProjectPackageHistoryList(ProjectHistoryState entity, EditProjectHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectPackageHistoryState> projectPackageHistoryListForDeletion = new List<ProjectPackageHistoryState>();
		var queryProjectPackageHistoryForDeletion = Context.ProjectPackageHistory.Where(l => l.ProjectHistoryId == request.Id).AsNoTracking();
		if (entity.ProjectPackageHistoryList?.Count > 0)
		{
			queryProjectPackageHistoryForDeletion = queryProjectPackageHistoryForDeletion.Where(l => !(entity.ProjectPackageHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectPackageHistoryListForDeletion = await queryProjectPackageHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectPackageHistory in projectPackageHistoryListForDeletion!)
		{
			Context.Entry(projectPackageHistory).State = EntityState.Deleted;
		}
		if (entity.ProjectPackageHistoryList?.Count > 0)
		{
			foreach (var projectPackageHistory in entity.ProjectPackageHistoryList.Where(l => !projectPackageHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectPackageHistoryState>(x => x.Id == projectPackageHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectPackageHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectPackageHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditProjectHistoryCommandValidator : AbstractValidator<EditProjectHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditProjectHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectHistory with id {PropertyValue} does not exists");
        
    }
}
