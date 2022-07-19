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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;

public record DeleteMainModuleCommand : BaseCommand, IRequest<Validation<Error, MainModuleState>>;

public class DeleteMainModuleCommandHandler : BaseCommandHandler<ApplicationContext, MainModuleState, DeleteMainModuleCommand>, IRequestHandler<DeleteMainModuleCommand, Validation<Error, MainModuleState>>
{
    public DeleteMainModuleCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteMainModuleCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModuleState>> Handle(DeleteMainModuleCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteMainModuleCommandValidator : AbstractValidator<DeleteMainModuleCommand>
{
    readonly ApplicationContext _context;

    public DeleteMainModuleCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MainModuleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModule with id {PropertyValue} does not exists");
    }
}
