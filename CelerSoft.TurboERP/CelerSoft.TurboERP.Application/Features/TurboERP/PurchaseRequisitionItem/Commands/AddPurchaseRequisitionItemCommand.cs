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

public record AddPurchaseRequisitionItemCommand : PurchaseRequisitionItemState, IRequest<Validation<Error, PurchaseRequisitionItemState>>;

public class AddPurchaseRequisitionItemCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseRequisitionItemState, AddPurchaseRequisitionItemCommand>, IRequestHandler<AddPurchaseRequisitionItemCommand, Validation<Error, PurchaseRequisitionItemState>>
{
	private readonly IdentityContext _identityContext;
    public AddPurchaseRequisitionItemCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPurchaseRequisitionItemCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PurchaseRequisitionItemState>> Handle(AddPurchaseRequisitionItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPurchaseRequisitionItemCommandValidator : AbstractValidator<AddPurchaseRequisitionItemCommand>
{
    readonly ApplicationContext _context;

    public AddPurchaseRequisitionItemCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PurchaseRequisitionItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseRequisitionItem with id {PropertyValue} already exists");
        
    }
}
