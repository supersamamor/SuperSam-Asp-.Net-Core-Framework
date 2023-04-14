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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Commands;

public record AddCustomerContactPersonCommand : CustomerContactPersonState, IRequest<Validation<Error, CustomerContactPersonState>>;

public class AddCustomerContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, CustomerContactPersonState, AddCustomerContactPersonCommand>, IRequestHandler<AddCustomerContactPersonCommand, Validation<Error, CustomerContactPersonState>>
{
	private readonly IdentityContext _identityContext;
    public AddCustomerContactPersonCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCustomerContactPersonCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, CustomerContactPersonState>> Handle(AddCustomerContactPersonCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddCustomerContactPersonCommandValidator : AbstractValidator<AddCustomerContactPersonCommand>
{
    readonly ApplicationContext _context;

    public AddCustomerContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CustomerContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CustomerContactPerson with id {PropertyValue} already exists");
        
    }
}
