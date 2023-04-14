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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Commands;

public record EditPurchaseCommand : PurchaseState, IRequest<Validation<Error, PurchaseState>>;

public class EditPurchaseCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseState, EditPurchaseCommand>, IRequestHandler<EditPurchaseCommand, Validation<Error, PurchaseState>>
{
    public EditPurchaseCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPurchaseCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PurchaseState>> Handle(EditPurchaseCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPurchaseCommandValidator : AbstractValidator<EditPurchaseCommand>
{
    readonly ApplicationContext _context;

    public EditPurchaseCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Purchase with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<PurchaseState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Purchase with code {PropertyValue} already exists");
	
    }
}
