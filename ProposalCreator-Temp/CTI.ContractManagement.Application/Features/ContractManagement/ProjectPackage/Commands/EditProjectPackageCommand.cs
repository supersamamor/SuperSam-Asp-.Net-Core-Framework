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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Commands;

public record EditProjectPackageCommand : ProjectPackageState, IRequest<Validation<Error, ProjectPackageState>>;

public class EditProjectPackageCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageState, EditProjectPackageCommand>, IRequestHandler<EditProjectPackageCommand, Validation<Error, ProjectPackageState>>
{
    public EditProjectPackageCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectPackageCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectPackageState>> Handle(EditProjectPackageCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditProjectPackage(request, cancellationToken));


	public async Task<Validation<Error, ProjectPackageState>> EditProjectPackage(EditProjectPackageCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ProjectPackage.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProjectPackageAdditionalDeliverableList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectPackageState>(entity);
	}
	
	private async Task UpdateProjectPackageAdditionalDeliverableList(ProjectPackageState entity, EditProjectPackageCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectPackageAdditionalDeliverableState> projectPackageAdditionalDeliverableListForDeletion = new List<ProjectPackageAdditionalDeliverableState>();
		var queryProjectPackageAdditionalDeliverableForDeletion = Context.ProjectPackageAdditionalDeliverable.Where(l => l.ProjectPackageId == request.Id).AsNoTracking();
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
	
}

public class EditProjectPackageCommandValidator : AbstractValidator<EditProjectPackageCommand>
{
    readonly ApplicationContext _context;

    public EditProjectPackageCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackage with id {PropertyValue} does not exists");
        
    }
}
