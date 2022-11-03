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

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Commands;

public record AddLeadTaskClientFeedBackCommand : LeadTaskClientFeedBackState, IRequest<Validation<Error, LeadTaskClientFeedBackState>>;

public class AddLeadTaskClientFeedBackCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskClientFeedBackState, AddLeadTaskClientFeedBackCommand>, IRequestHandler<AddLeadTaskClientFeedBackCommand, Validation<Error, LeadTaskClientFeedBackState>>
{
	private readonly IdentityContext _identityContext;
    public AddLeadTaskClientFeedBackCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLeadTaskClientFeedBackCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, LeadTaskClientFeedBackState>> Handle(AddLeadTaskClientFeedBackCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddLeadTaskClientFeedBackCommandValidator : AbstractValidator<AddLeadTaskClientFeedBackCommand>
{
    readonly ApplicationContext _context;

    public AddLeadTaskClientFeedBackCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LeadTaskClientFeedBackState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTaskClientFeedBack with id {PropertyValue} already exists");
        
    }
}
