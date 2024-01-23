using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Commands;

public record AddContactInformationCommand : ContactInformationState, IRequest<Validation<Error, ContactInformationState>>;

public class AddContactInformationCommandHandler : BaseCommandHandler<ApplicationContext, ContactInformationState, AddContactInformationCommand>, IRequestHandler<AddContactInformationCommand, Validation<Error, ContactInformationState>>
{
	private readonly IdentityContext _identityContext;
    public AddContactInformationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddContactInformationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ContactInformationState>> Handle(AddContactInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddContactInformationCommandValidator : AbstractValidator<AddContactInformationCommand>
{
    readonly ApplicationContext _context;

    public AddContactInformationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ContactInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ContactInformation with id {PropertyValue} already exists");
        
    }
}
