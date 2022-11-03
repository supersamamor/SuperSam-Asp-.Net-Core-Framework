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

namespace CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Commands;

public record EditIFCATransactionTypeCommand : IFCATransactionTypeState, IRequest<Validation<Error, IFCATransactionTypeState>>;

public class EditIFCATransactionTypeCommandHandler : BaseCommandHandler<ApplicationContext, IFCATransactionTypeState, EditIFCATransactionTypeCommand>, IRequestHandler<EditIFCATransactionTypeCommand, Validation<Error, IFCATransactionTypeState>>
{
    public EditIFCATransactionTypeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditIFCATransactionTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, IFCATransactionTypeState>> Handle(EditIFCATransactionTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditIFCATransactionTypeCommandValidator : AbstractValidator<EditIFCATransactionTypeCommand>
{
    readonly ApplicationContext _context;

    public EditIFCATransactionTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCATransactionTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCATransactionType with id {PropertyValue} does not exists");
        
    }
}
