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

namespace CTI.ELMS.Application.Features.ELMS.Lead.Commands;

public record AddLeadCommand : LeadState, IRequest<Validation<Error, LeadState>>;

public class AddLeadCommandHandler : BaseCommandHandler<ApplicationContext, LeadState, AddLeadCommand>, IRequestHandler<AddLeadCommand, Validation<Error, LeadState>>
{
	private readonly IdentityContext _identityContext;
    public AddLeadCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLeadCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, LeadState>> Handle(AddLeadCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddLead(request, cancellationToken));


	public async Task<Validation<Error, LeadState>> AddLead(AddLeadCommand request, CancellationToken cancellationToken)
	{
		LeadState entity = Mapper.Map<LeadState>(request);
		UpdateContactList(entity);
		UpdateContactPersonList(entity);	
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, LeadState>(entity);
	}
	
	private void UpdateContactList(LeadState entity)
	{
		if (entity.ContactList?.Count > 0)
		{
			foreach (var contact in entity.ContactList!)
			{
				Context.Entry(contact).State = EntityState.Added;
			}
		}
	}
	private void UpdateContactPersonList(LeadState entity)
	{
		if (entity.ContactPersonList?.Count > 0)
		{
			foreach (var contactPerson in entity.ContactPersonList!)
			{
				Context.Entry(contactPerson).State = EntityState.Added;
			}
		}
	}
}

public class AddLeadCommandValidator : AbstractValidator<AddLeadCommand>
{
    readonly ApplicationContext _context;

    public AddLeadCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LeadState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Lead with id {PropertyValue} already exists");
        
    }
}
