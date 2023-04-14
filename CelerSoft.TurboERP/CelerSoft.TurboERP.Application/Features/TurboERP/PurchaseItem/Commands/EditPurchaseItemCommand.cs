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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Commands;

public record EditPurchaseItemCommand : PurchaseItemState, IRequest<Validation<Error, PurchaseItemState>>;

public class EditPurchaseItemCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseItemState, EditPurchaseItemCommand>, IRequestHandler<EditPurchaseItemCommand, Validation<Error, PurchaseItemState>>
{
    public EditPurchaseItemCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPurchaseItemCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PurchaseItemState>> Handle(EditPurchaseItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPurchaseItemCommandValidator : AbstractValidator<EditPurchaseItemCommand>
{
    readonly ApplicationContext _context;

    public EditPurchaseItemCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseItem with id {PropertyValue} does not exists");
        
    }
}
