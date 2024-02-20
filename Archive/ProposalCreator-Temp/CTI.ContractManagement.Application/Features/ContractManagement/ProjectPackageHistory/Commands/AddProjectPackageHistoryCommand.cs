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

public record AddProjectPackageHistoryCommand : ProjectPackageHistoryState, IRequest<Validation<Error, ProjectPackageHistoryState>>;

public class AddProjectPackageHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageHistoryState, AddProjectPackageHistoryCommand>, IRequestHandler<AddProjectPackageHistoryCommand, Validation<Error, ProjectPackageHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectPackageHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectPackageHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ProjectPackageHistoryState>> Handle(AddProjectPackageHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddProjectPackageHistory(request, cancellationToken));


	public async Task<Validation<Error, ProjectPackageHistoryState>> AddProjectPackageHistory(AddProjectPackageHistoryCommand request, CancellationToken cancellationToken)
	{
		ProjectPackageHistoryState entity = Mapper.Map<ProjectPackageHistoryState>(request);
		UpdateProjectPackageAdditionalDeliverableHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectPackageHistoryState>(entity);
	}
	
	private void UpdateProjectPackageAdditionalDeliverableHistoryList(ProjectPackageHistoryState entity)
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

public class AddProjectPackageHistoryCommandValidator : AbstractValidator<AddProjectPackageHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddProjectPackageHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectPackageHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageHistory with id {PropertyValue} already exists");
        
    }
}
