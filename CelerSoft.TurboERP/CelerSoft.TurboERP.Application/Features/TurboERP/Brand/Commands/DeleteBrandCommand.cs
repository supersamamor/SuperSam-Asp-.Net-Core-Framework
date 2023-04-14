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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Brand.Commands;

public record DeleteBrandCommand : BaseCommand, IRequest<Validation<Error, BrandState>>;

public class DeleteBrandCommandHandler : BaseCommandHandler<ApplicationContext, BrandState, DeleteBrandCommand>, IRequestHandler<DeleteBrandCommand, Validation<Error, BrandState>>
{
    public DeleteBrandCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBrandCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BrandState>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    readonly ApplicationContext _context;

    public DeleteBrandCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BrandState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Brand with id {PropertyValue} does not exists");
    }
}
