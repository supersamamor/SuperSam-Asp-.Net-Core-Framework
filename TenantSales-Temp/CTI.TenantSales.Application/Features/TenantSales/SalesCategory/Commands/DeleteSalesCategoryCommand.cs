using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Commands;

public record DeleteSalesCategoryCommand : BaseCommand, IRequest<Validation<Error, SalesCategoryState>>;

public class DeleteSalesCategoryCommandHandler : BaseCommandHandler<ApplicationContext, SalesCategoryState, DeleteSalesCategoryCommand>, IRequestHandler<DeleteSalesCategoryCommand, Validation<Error, SalesCategoryState>>
{
    public DeleteSalesCategoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSalesCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SalesCategoryState>> Handle(DeleteSalesCategoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSalesCategoryCommandValidator : AbstractValidator<DeleteSalesCategoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteSalesCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SalesCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SalesCategory with id {PropertyValue} does not exists");
    }
}
