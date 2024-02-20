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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Commands;

public record AddProjectCategoryCommand : ProjectCategoryState, IRequest<Validation<Error, ProjectCategoryState>>;

public class AddProjectCategoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectCategoryState, AddProjectCategoryCommand>, IRequestHandler<AddProjectCategoryCommand, Validation<Error, ProjectCategoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectCategoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectCategoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProjectCategoryState>> Handle(AddProjectCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectCategoryCommandValidator : AbstractValidator<AddProjectCategoryCommand>
{
    readonly ApplicationContext _context;

    public AddProjectCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectCategory with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<ProjectCategoryState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("ProjectCategory with name {PropertyValue} already exists");
	
    }
}
