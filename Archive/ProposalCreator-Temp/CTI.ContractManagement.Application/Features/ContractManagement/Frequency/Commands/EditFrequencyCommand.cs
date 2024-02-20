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

namespace CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Commands;

public record EditFrequencyCommand : FrequencyState, IRequest<Validation<Error, FrequencyState>>;

public class EditFrequencyCommandHandler : BaseCommandHandler<ApplicationContext, FrequencyState, EditFrequencyCommand>, IRequestHandler<EditFrequencyCommand, Validation<Error, FrequencyState>>
{
    public EditFrequencyCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditFrequencyCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, FrequencyState>> Handle(EditFrequencyCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditFrequency(request, cancellationToken));


	public async Task<Validation<Error, FrequencyState>> EditFrequency(EditFrequencyCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Frequency.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProjectMilestoneList(entity, request, cancellationToken);
		await UpdateProjectMilestoneHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, FrequencyState>(entity);
	}
	
	private async Task UpdateProjectMilestoneList(FrequencyState entity, EditFrequencyCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectMilestoneState> projectMilestoneListForDeletion = new List<ProjectMilestoneState>();
		var queryProjectMilestoneForDeletion = Context.ProjectMilestone.Where(l => l.FrequencyId == request.Id).AsNoTracking();
		if (entity.ProjectMilestoneList?.Count > 0)
		{
			queryProjectMilestoneForDeletion = queryProjectMilestoneForDeletion.Where(l => !(entity.ProjectMilestoneList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectMilestoneListForDeletion = await queryProjectMilestoneForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectMilestone in projectMilestoneListForDeletion!)
		{
			Context.Entry(projectMilestone).State = EntityState.Deleted;
		}
		if (entity.ProjectMilestoneList?.Count > 0)
		{
			foreach (var projectMilestone in entity.ProjectMilestoneList.Where(l => !projectMilestoneListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectMilestoneState>(x => x.Id == projectMilestone.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectMilestone).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectMilestone).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateProjectMilestoneHistoryList(FrequencyState entity, EditFrequencyCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectMilestoneHistoryState> projectMilestoneHistoryListForDeletion = new List<ProjectMilestoneHistoryState>();
		var queryProjectMilestoneHistoryForDeletion = Context.ProjectMilestoneHistory.Where(l => l.FrequencyId == request.Id).AsNoTracking();
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
	
}

public class EditFrequencyCommandValidator : AbstractValidator<EditFrequencyCommand>
{
    readonly ApplicationContext _context;

    public EditFrequencyCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FrequencyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Frequency with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<FrequencyState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Frequency with name {PropertyValue} already exists");
	
    }
}
