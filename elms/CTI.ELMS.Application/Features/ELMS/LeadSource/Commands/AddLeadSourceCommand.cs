using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.LeadSource.Commands;

public record AddLeadSourceCommand : LeadSourceState, IRequest<Validation<Error, LeadSourceState>>;

public class AddLeadSourceCommandHandler : BaseCommandHandler<ApplicationContext, LeadSourceState, AddLeadSourceCommand>, IRequestHandler<AddLeadSourceCommand, Validation<Error, LeadSourceState>>
{
	private readonly IdentityContext _identityContext;
    public AddLeadSourceCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLeadSourceCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, LeadSourceState>> Handle(AddLeadSourceCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddLeadSourceCommandValidator : AbstractValidator<AddLeadSourceCommand>
{
    readonly ApplicationContext _context;

    public AddLeadSourceCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LeadSourceState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadSource with id {PropertyValue} already exists");
        RuleFor(x => x.LeadSourceName).MustAsync(async (leadSourceName, cancellation) => await _context.NotExists<LeadSourceState>(x => x.LeadSourceName == leadSourceName, cancellationToken: cancellation)).WithMessage("LeadSource with leadSourceName {PropertyValue} already exists");
	
    }
}
