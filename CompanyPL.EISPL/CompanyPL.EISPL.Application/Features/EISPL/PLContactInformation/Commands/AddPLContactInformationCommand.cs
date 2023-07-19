using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Commands;

public record AddPLContactInformationCommand : PLContactInformationState, IRequest<Validation<Error, PLContactInformationState>>;

public class AddPLContactInformationCommandHandler : BaseCommandHandler<ApplicationContext, PLContactInformationState, AddPLContactInformationCommand>, IRequestHandler<AddPLContactInformationCommand, Validation<Error, PLContactInformationState>>
{
	private readonly IdentityContext _identityContext;
    public AddPLContactInformationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPLContactInformationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PLContactInformationState>> Handle(AddPLContactInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPLContactInformationCommandValidator : AbstractValidator<AddPLContactInformationCommand>
{
    readonly ApplicationContext _context;

    public AddPLContactInformationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PLContactInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLContactInformation with id {PropertyValue} already exists");
        
    }
}
