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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Commands;

public record EditPurchaseRequisitionItemCommand : PurchaseRequisitionItemState, IRequest<Validation<Error, PurchaseRequisitionItemState>>;

public class EditPurchaseRequisitionItemCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseRequisitionItemState, EditPurchaseRequisitionItemCommand>, IRequestHandler<EditPurchaseRequisitionItemCommand, Validation<Error, PurchaseRequisitionItemState>>
{
    public EditPurchaseRequisitionItemCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPurchaseRequisitionItemCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PurchaseRequisitionItemState>> Handle(EditPurchaseRequisitionItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPurchaseRequisitionItemCommandValidator : AbstractValidator<EditPurchaseRequisitionItemCommand>
{
    readonly ApplicationContext _context;

    public EditPurchaseRequisitionItemCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseRequisitionItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseRequisitionItem with id {PropertyValue} does not exists");
        
    }
}
