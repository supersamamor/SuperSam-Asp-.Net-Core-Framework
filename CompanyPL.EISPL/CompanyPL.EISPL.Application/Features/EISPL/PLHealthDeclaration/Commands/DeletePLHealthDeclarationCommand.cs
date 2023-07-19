using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Commands;

public record DeletePLHealthDeclarationCommand : BaseCommand, IRequest<Validation<Error, PLHealthDeclarationState>>;

public class DeletePLHealthDeclarationCommandHandler : BaseCommandHandler<ApplicationContext, PLHealthDeclarationState, DeletePLHealthDeclarationCommand>, IRequestHandler<DeletePLHealthDeclarationCommand, Validation<Error, PLHealthDeclarationState>>
{
    public DeletePLHealthDeclarationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePLHealthDeclarationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PLHealthDeclarationState>> Handle(DeletePLHealthDeclarationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePLHealthDeclarationCommandValidator : AbstractValidator<DeletePLHealthDeclarationCommand>
{
    readonly ApplicationContext _context;

    public DeletePLHealthDeclarationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PLHealthDeclarationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLHealthDeclaration with id {PropertyValue} does not exists");
    }
}
