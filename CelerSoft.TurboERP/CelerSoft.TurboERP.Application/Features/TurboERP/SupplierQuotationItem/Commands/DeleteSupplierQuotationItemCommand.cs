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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Commands;

public record DeleteSupplierQuotationItemCommand : BaseCommand, IRequest<Validation<Error, SupplierQuotationItemState>>;

public class DeleteSupplierQuotationItemCommandHandler : BaseCommandHandler<ApplicationContext, SupplierQuotationItemState, DeleteSupplierQuotationItemCommand>, IRequestHandler<DeleteSupplierQuotationItemCommand, Validation<Error, SupplierQuotationItemState>>
{
    public DeleteSupplierQuotationItemCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSupplierQuotationItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SupplierQuotationItemState>> Handle(DeleteSupplierQuotationItemCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSupplierQuotationItemCommandValidator : AbstractValidator<DeleteSupplierQuotationItemCommand>
{
    readonly ApplicationContext _context;

    public DeleteSupplierQuotationItemCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierQuotationItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierQuotationItem with id {PropertyValue} does not exists");
    }
}
