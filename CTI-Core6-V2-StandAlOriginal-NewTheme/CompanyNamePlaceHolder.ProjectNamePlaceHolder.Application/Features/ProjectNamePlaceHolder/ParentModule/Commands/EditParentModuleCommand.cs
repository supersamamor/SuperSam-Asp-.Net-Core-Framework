using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;

public record EditParentModuleCommand : ParentModuleState, IRequest<Validation<Error, ParentModuleState>>;

public class EditParentModuleCommandHandler : BaseCommandHandler<ApplicationContext, ParentModuleState, EditParentModuleCommand>, IRequestHandler<EditParentModuleCommand, Validation<Error, ParentModuleState>>
{
    public EditParentModuleCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditParentModuleCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ParentModuleState>> Handle(EditParentModuleCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditParentModuleCommandValidator : AbstractValidator<EditParentModuleCommand>
{
    readonly ApplicationContext _context;

    public EditParentModuleCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ParentModuleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ParentModule with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<ParentModuleState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ParentModule with name {PropertyValue} already exists");
	
    }
}
