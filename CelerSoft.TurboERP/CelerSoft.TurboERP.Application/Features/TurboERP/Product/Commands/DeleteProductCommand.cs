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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Product.Commands;

public record DeleteProductCommand : BaseCommand, IRequest<Validation<Error, ProductState>>;

public class DeleteProductCommandHandler : BaseCommandHandler<ApplicationContext, ProductState, DeleteProductCommand>, IRequestHandler<DeleteProductCommand, Validation<Error, ProductState>>
{
    public DeleteProductCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProductCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProductState>> Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    readonly ApplicationContext _context;

    public DeleteProductCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProductState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Product with id {PropertyValue} does not exists");
    }
}
