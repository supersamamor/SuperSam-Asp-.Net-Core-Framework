using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Commands;

public record DeleteSupplierContactPersonCommand : BaseCommand, IRequest<Validation<Error, SupplierContactPersonState>>;

public class DeleteSupplierContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, SupplierContactPersonState, DeleteSupplierContactPersonCommand>, IRequestHandler<DeleteSupplierContactPersonCommand, Validation<Error, SupplierContactPersonState>>
{
    public DeleteSupplierContactPersonCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSupplierContactPersonCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SupplierContactPersonState>> Handle(DeleteSupplierContactPersonCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSupplierContactPersonCommandValidator : AbstractValidator<DeleteSupplierContactPersonCommand>
{
    readonly ApplicationContext _context;

    public DeleteSupplierContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierContactPerson with id {PropertyValue} does not exists");
    }
}
