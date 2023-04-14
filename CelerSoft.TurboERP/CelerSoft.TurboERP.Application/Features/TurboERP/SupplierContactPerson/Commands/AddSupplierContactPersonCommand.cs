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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Commands;

public record AddSupplierContactPersonCommand : SupplierContactPersonState, IRequest<Validation<Error, SupplierContactPersonState>>;

public class AddSupplierContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, SupplierContactPersonState, AddSupplierContactPersonCommand>, IRequestHandler<AddSupplierContactPersonCommand, Validation<Error, SupplierContactPersonState>>
{
	private readonly IdentityContext _identityContext;
    public AddSupplierContactPersonCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSupplierContactPersonCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, SupplierContactPersonState>> Handle(AddSupplierContactPersonCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddSupplierContactPersonCommandValidator : AbstractValidator<AddSupplierContactPersonCommand>
{
    readonly ApplicationContext _context;

    public AddSupplierContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SupplierContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierContactPerson with id {PropertyValue} already exists");
        
    }
}
