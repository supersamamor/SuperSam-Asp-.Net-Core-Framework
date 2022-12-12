using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.FAS.Application.Features.FAS.Bank.Commands;

public record DeleteBankCommand : BaseCommand, IRequest<Validation<Error, BankState>>;

public class DeleteBankCommandHandler : BaseCommandHandler<ApplicationContext, BankState, DeleteBankCommand>, IRequestHandler<DeleteBankCommand, Validation<Error, BankState>>
{
    public DeleteBankCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBankCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BankState>> Handle(DeleteBankCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBankCommandValidator : AbstractValidator<DeleteBankCommand>
{
    readonly ApplicationContext _context;

    public DeleteBankCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BankState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Bank with id {PropertyValue} does not exists");
    }
}
