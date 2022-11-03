using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Commands;

public record AddIFCAARLedgerCommand : IFCAARLedgerState, IRequest<Validation<Error, IFCAARLedgerState>>;

public class AddIFCAARLedgerCommandHandler : BaseCommandHandler<ApplicationContext, IFCAARLedgerState, AddIFCAARLedgerCommand>, IRequestHandler<AddIFCAARLedgerCommand, Validation<Error, IFCAARLedgerState>>
{
	private readonly IdentityContext _identityContext;
    public AddIFCAARLedgerCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddIFCAARLedgerCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, IFCAARLedgerState>> Handle(AddIFCAARLedgerCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddIFCAARLedgerCommandValidator : AbstractValidator<AddIFCAARLedgerCommand>
{
    readonly ApplicationContext _context;

    public AddIFCAARLedgerCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<IFCAARLedgerState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAARLedger with id {PropertyValue} already exists");
        
    }
}
