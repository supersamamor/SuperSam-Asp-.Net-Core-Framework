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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;

public record DeleteTenantPOSSalesCommand : BaseCommand, IRequest<Validation<Error, TenantPOSSalesState>>;

public class DeleteTenantPOSSalesCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSSalesState, DeleteTenantPOSSalesCommand>, IRequestHandler<DeleteTenantPOSSalesCommand, Validation<Error, TenantPOSSalesState>>
{
    public DeleteTenantPOSSalesCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTenantPOSSalesCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TenantPOSSalesState>> Handle(DeleteTenantPOSSalesCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTenantPOSSalesCommandValidator : AbstractValidator<DeleteTenantPOSSalesCommand>
{
    readonly ApplicationContext _context;

    public DeleteTenantPOSSalesCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantPOSSalesState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantPOSSales with id {PropertyValue} does not exists");
    }
}
