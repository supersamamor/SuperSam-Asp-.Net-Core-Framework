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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Commands;

public record EditProjectPackageHistoryCommand : ProjectPackageHistoryState, IRequest<Validation<Error, ProjectPackageHistoryState>>;

public class EditProjectPackageHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageHistoryState, EditProjectPackageHistoryCommand>, IRequestHandler<EditProjectPackageHistoryCommand, Validation<Error, ProjectPackageHistoryState>>
{
    public EditProjectPackageHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectPackageHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectPackageHistoryState>> Handle(EditProjectPackageHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditProjectPackageHistory(request, cancellationToken));


	public async Task<Validation<Error, ProjectPackageHistoryState>> EditProjectPackageHistory(EditProjectPackageHistoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ProjectPackageHistory.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProjectPackageAdditionalDeliverableHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectPackageHistoryState>(entity);
	}
	
	private async Task UpdateProjectPackageAdditionalDeliverableHistoryList(ProjectPackageHistoryState entity, EditProjectPackageHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectPackageAdditionalDeliverableHistoryState> projectPackageAdditionalDeliverableHistoryListForDeletion = new List<ProjectPackageAdditionalDeliverableHistoryState>();
		var queryProjectPackageAdditionalDeliverableHistoryForDeletion = Context.ProjectPackageAdditionalDeliverableHistory.Where(l => l.ProjectPackageHistoryId == request.Id).AsNoTracking();
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

public class EditProjectPackageHistoryCommandValidator : AbstractValidator<EditProjectPackageHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditProjectPackageHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageHistory with id {PropertyValue} does not exists");
        
    }
}
