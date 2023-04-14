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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Commands;

public record AddInventoryHistoryCommand : InventoryHistoryState, IRequest<Validation<Error, InventoryHistoryState>>;

public class AddInventoryHistoryCommandHandler : BaseCommandHandler<ApplicationContext, InventoryHistoryState, AddInventoryHistoryCommand>, IRequestHandler<AddInventoryHistoryCommand, Validation<Error, InventoryHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddInventoryHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddInventoryHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, InventoryHistoryState>> Handle(AddInventoryHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddInventoryHistoryCommandValidator : AbstractValidator<AddInventoryHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddInventoryHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<InventoryHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("InventoryHistory with id {PropertyValue} already exists");
        
    }
}
