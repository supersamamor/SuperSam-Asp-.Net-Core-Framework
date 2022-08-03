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

namespace CTI.TenantSales.Application.Features.TenantSales.Revalidate.Commands;

public record DeleteRevalidateCommand : BaseCommand, IRequest<Validation<Error, RevalidateState>>;

public class DeleteRevalidateCommandHandler : BaseCommandHandler<ApplicationContext, RevalidateState, DeleteRevalidateCommand>, IRequestHandler<DeleteRevalidateCommand, Validation<Error, RevalidateState>>
{
    public DeleteRevalidateCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteRevalidateCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, RevalidateState>> Handle(DeleteRevalidateCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteRevalidateCommandValidator : AbstractValidator<DeleteRevalidateCommand>
{
    readonly ApplicationContext _context;

    public DeleteRevalidateCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RevalidateState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Revalidate with id {PropertyValue} does not exists");
    }
}
