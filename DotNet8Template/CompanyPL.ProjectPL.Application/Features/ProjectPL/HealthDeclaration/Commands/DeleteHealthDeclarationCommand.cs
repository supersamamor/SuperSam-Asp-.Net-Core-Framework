using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;

public record DeleteHealthDeclarationCommand : BaseCommand, IRequest<Validation<Error, HealthDeclarationState>>;

public class DeleteHealthDeclarationCommandHandler : BaseCommandHandler<ApplicationContext, HealthDeclarationState, DeleteHealthDeclarationCommand>, IRequestHandler<DeleteHealthDeclarationCommand, Validation<Error, HealthDeclarationState>>
{
    public DeleteHealthDeclarationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteHealthDeclarationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, HealthDeclarationState>> Handle(DeleteHealthDeclarationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteHealthDeclarationCommandValidator : AbstractValidator<DeleteHealthDeclarationCommand>
{
    readonly ApplicationContext _context;

    public DeleteHealthDeclarationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<HealthDeclarationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("HealthDeclaration with id {PropertyValue} does not exists");
    }
}
