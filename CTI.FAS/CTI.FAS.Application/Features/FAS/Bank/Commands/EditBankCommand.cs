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

public record EditBankCommand : BankState, IRequest<Validation<Error, BankState>>;

public class EditBankCommandHandler : BaseCommandHandler<ApplicationContext, BankState, EditBankCommand>, IRequestHandler<EditBankCommand, Validation<Error, BankState>>
{
    public EditBankCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBankCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, BankState>> Handle(EditBankCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditBankCommandValidator : AbstractValidator<EditBankCommand>
{
    readonly ApplicationContext _context;

    public EditBankCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BankState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Bank with id {PropertyValue} does not exists");
        
    }
}
