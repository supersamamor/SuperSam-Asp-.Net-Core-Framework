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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;

public record DeleteParentModuleCommand : BaseCommand, IRequest<Validation<Error, ParentModuleState>>;

public class DeleteParentModuleCommandHandler : BaseCommandHandler<ApplicationContext, ParentModuleState, DeleteParentModuleCommand>, IRequestHandler<DeleteParentModuleCommand, Validation<Error, ParentModuleState>>
{
    public DeleteParentModuleCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteParentModuleCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ParentModuleState>> Handle(DeleteParentModuleCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteParentModuleCommandValidator : AbstractValidator<DeleteParentModuleCommand>
{
    readonly ApplicationContext _context;

    public DeleteParentModuleCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ParentModuleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ParentModule with id {PropertyValue} does not exists");
    }
}
