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

public record EditCustomerContactPersonCommand : CustomerContactPersonState, IRequest<Validation<Error, CustomerContactPersonState>>;

public class EditCustomerContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, CustomerContactPersonState, EditCustomerContactPersonCommand>, IRequestHandler<EditCustomerContactPersonCommand, Validation<Error, CustomerContactPersonState>>
{
    public EditCustomerContactPersonCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCustomerContactPersonCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CustomerContactPersonState>> Handle(EditCustomerContactPersonCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCustomerContactPersonCommandValidator : AbstractValidator<EditCustomerContactPersonCommand>
{
    readonly ApplicationContext _context;

    public EditCustomerContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CustomerContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CustomerContactPerson with id {PropertyValue} does not exists");
        
    }
}
