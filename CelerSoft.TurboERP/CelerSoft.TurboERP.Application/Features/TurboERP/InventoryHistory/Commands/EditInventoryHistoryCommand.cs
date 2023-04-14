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

public record EditInventoryHistoryCommand : InventoryHistoryState, IRequest<Validation<Error, InventoryHistoryState>>;

public class EditInventoryHistoryCommandHandler : BaseCommandHandler<ApplicationContext, InventoryHistoryState, EditInventoryHistoryCommand>, IRequestHandler<EditInventoryHistoryCommand, Validation<Error, InventoryHistoryState>>
{
    public EditInventoryHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditInventoryHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, InventoryHistoryState>> Handle(EditInventoryHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditInventoryHistoryCommandValidator : AbstractValidator<EditInventoryHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditInventoryHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<InventoryHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("InventoryHistory with id {PropertyValue} does not exists");
        
    }
}
