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

public record AddProjectPackageCommand : ProjectPackageState, IRequest<Validation<Error, ProjectPackageState>>;

public class AddProjectPackageCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageState, AddProjectPackageCommand>, IRequestHandler<AddProjectPackageCommand, Validation<Error, ProjectPackageState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectPackageCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectPackageCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ProjectPackageState>> Handle(AddProjectPackageCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddProjectPackage(request, cancellationToken));


	public async Task<Validation<Error, ProjectPackageState>> AddProjectPackage(AddProjectPackageCommand request, CancellationToken cancellationToken)
	{
		ProjectPackageState entity = Mapper.Map<ProjectPackageState>(request);
		UpdateProjectPackageAdditionalDeliverableList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectPackageState>(entity);
	}
	
	private void UpdateProjectPackageAdditionalDeliverableList(ProjectPackageState entity)
	{
		if (entity.ProjectPackageAdditionalDeliverableList?.Count > 0)
		{
			foreach (var projectPackageAdditionalDeliverable in entity.ProjectPackageAdditionalDeliverableList!)
			{
				Context.Entry(projectPackageAdditionalDeliverable).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddProjectPackageCommandValidator : AbstractValidator<AddProjectPackageCommand>
{
    readonly ApplicationContext _context;

    public AddProjectPackageCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectPackageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackage with id {PropertyValue} already exists");
        
    }
}
