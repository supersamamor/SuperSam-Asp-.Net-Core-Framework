using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;

public record AddParentModuleCommand : ParentModuleState, IRequest<Validation<Error, ParentModuleState>>;

public class AddParentModuleCommandHandler : BaseCommandHandler<ApplicationContext, ParentModuleState, AddParentModuleCommand>, IRequestHandler<AddParentModuleCommand, Validation<Error, ParentModuleState>>
{
    public AddParentModuleCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddParentModuleCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ParentModuleState>> Handle(AddParentModuleCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddParentModuleCommandValidator : AbstractValidator<AddParentModuleCommand>
{
    readonly ApplicationContext _context;

    public AddParentModuleCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ParentModuleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ParentModule with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<ParentModuleState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("ParentModule with name {PropertyValue} already exists");
	
    }
}
