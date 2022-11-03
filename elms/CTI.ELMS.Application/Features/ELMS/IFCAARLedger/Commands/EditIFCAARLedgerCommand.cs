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

public record EditIFCAARLedgerCommand : IFCAARLedgerState, IRequest<Validation<Error, IFCAARLedgerState>>;

public class EditIFCAARLedgerCommandHandler : BaseCommandHandler<ApplicationContext, IFCAARLedgerState, EditIFCAARLedgerCommand>, IRequestHandler<EditIFCAARLedgerCommand, Validation<Error, IFCAARLedgerState>>
{
    public EditIFCAARLedgerCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditIFCAARLedgerCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, IFCAARLedgerState>> Handle(EditIFCAARLedgerCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditIFCAARLedgerCommandValidator : AbstractValidator<EditIFCAARLedgerCommand>
{
    readonly ApplicationContext _context;

    public EditIFCAARLedgerCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCAARLedgerState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAARLedger with id {PropertyValue} does not exists");
        
    }
}
