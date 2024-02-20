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

public record EditClientCommand : ClientState, IRequest<Validation<Error, ClientState>>;

public class EditClientCommandHandler : BaseCommandHandler<ApplicationContext, ClientState, EditClientCommand>, IRequestHandler<EditClientCommand, Validation<Error, ClientState>>
{
    public EditClientCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditClientCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ClientState>> Handle(EditClientCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditClientCommandValidator : AbstractValidator<EditClientCommand>
{
    readonly ApplicationContext _context;

    public EditClientCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ClientState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Client with id {PropertyValue} does not exists");
        RuleFor(x => x.ContactPersonName).MustAsync(async (request, contactPersonName, cancellation) => await _context.NotExists<ClientState>(x => x.ContactPersonName == contactPersonName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Client with contactPersonName {PropertyValue} already exists");
	RuleFor(x => x.EmailAddress).MustAsync(async (request, emailAddress, cancellation) => await _context.NotExists<ClientState>(x => x.EmailAddress == emailAddress && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Client with emailAddress {PropertyValue} already exists");
	
    }
}
