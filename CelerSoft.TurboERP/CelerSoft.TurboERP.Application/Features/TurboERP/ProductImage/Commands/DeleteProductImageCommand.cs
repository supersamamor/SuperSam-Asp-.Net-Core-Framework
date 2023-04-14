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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Commands;

public record DeleteProductImageCommand : BaseCommand, IRequest<Validation<Error, ProductImageState>>;

public class DeleteProductImageCommandHandler : BaseCommandHandler<ApplicationContext, ProductImageState, DeleteProductImageCommand>, IRequestHandler<DeleteProductImageCommand, Validation<Error, ProductImageState>>
{
    public DeleteProductImageCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProductImageCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProductImageState>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProductImageCommandValidator : AbstractValidator<DeleteProductImageCommand>
{
    readonly ApplicationContext _context;

    public DeleteProductImageCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProductImageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProductImage with id {PropertyValue} does not exists");
    }
}
