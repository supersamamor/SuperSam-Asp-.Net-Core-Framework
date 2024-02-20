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

public record EditProjectCategoryCommand : ProjectCategoryState, IRequest<Validation<Error, ProjectCategoryState>>;

public class EditProjectCategoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectCategoryState, EditProjectCategoryCommand>, IRequestHandler<EditProjectCategoryCommand, Validation<Error, ProjectCategoryState>>
{
    public EditProjectCategoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectCategoryState>> Handle(EditProjectCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectCategoryCommandValidator : AbstractValidator<EditProjectCategoryCommand>
{
    readonly ApplicationContext _context;

    public EditProjectCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectCategory with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<ProjectCategoryState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ProjectCategory with name {PropertyValue} already exists");
	
    }
}
