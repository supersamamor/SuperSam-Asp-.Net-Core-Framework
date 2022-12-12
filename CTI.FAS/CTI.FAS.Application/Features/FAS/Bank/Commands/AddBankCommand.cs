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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.Bank.Commands;

public record AddBankCommand : BankState, IRequest<Validation<Error, BankState>>;

public class AddBankCommandHandler : BaseCommandHandler<ApplicationContext, BankState, AddBankCommand>, IRequestHandler<AddBankCommand, Validation<Error, BankState>>
{
	private readonly IdentityContext _identityContext;
    public AddBankCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBankCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, BankState>> Handle(AddBankCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddBankCommandValidator : AbstractValidator<AddBankCommand>
{
    readonly ApplicationContext _context;

    public AddBankCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BankState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Bank with id {PropertyValue} already exists");
        
    }
}
