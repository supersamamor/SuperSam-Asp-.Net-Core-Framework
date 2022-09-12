using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Client.Commands;

public record AddClientCommand : ClientState, IRequest<Validation<Error, ClientState>>;

public class AddClientCommandHandler : BaseCommandHandler<ApplicationContext, ClientState, AddClientCommand>, IRequestHandler<AddClientCommand, Validation<Error, ClientState>>
{
	private readonly IdentityContext _identityContext;
    public AddClientCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddClientCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ClientState>> Handle(AddClientCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddClientCommandValidator : AbstractValidator<AddClientCommand>
{
    readonly ApplicationContext _context;

    public AddClientCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ClientState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Client with id {PropertyValue} already exists");
        RuleFor(x => x.ContactPersonName).MustAsync(async (contactPersonName, cancellation) => await _context.NotExists<ClientState>(x => x.ContactPersonName == contactPersonName, cancellationToken: cancellation)).WithMessage("Client with contactPersonName {PropertyValue} already exists");
	RuleFor(x => x.EmailAddress).MustAsync(async (emailAddress, cancellation) => await _context.NotExists<ClientState>(x => x.EmailAddress == emailAddress, cancellationToken: cancellation)).WithMessage("Client with emailAddress {PropertyValue} already exists");
	
    }
}
