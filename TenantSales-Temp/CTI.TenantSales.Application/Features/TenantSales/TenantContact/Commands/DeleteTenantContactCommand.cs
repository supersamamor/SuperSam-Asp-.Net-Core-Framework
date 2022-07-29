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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantContact.Commands;

public record DeleteTenantContactCommand : BaseCommand, IRequest<Validation<Error, TenantContactState>>;

public class DeleteTenantContactCommandHandler : BaseCommandHandler<ApplicationContext, TenantContactState, DeleteTenantContactCommand>, IRequestHandler<DeleteTenantContactCommand, Validation<Error, TenantContactState>>
{
    public DeleteTenantContactCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTenantContactCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TenantContactState>> Handle(DeleteTenantContactCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTenantContactCommandValidator : AbstractValidator<DeleteTenantContactCommand>
{
    readonly ApplicationContext _context;

    public DeleteTenantContactCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantContactState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantContact with id {PropertyValue} does not exists");
    }
}
