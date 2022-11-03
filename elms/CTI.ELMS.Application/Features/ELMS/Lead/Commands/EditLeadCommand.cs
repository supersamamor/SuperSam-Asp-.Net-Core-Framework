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

public record EditLeadCommand : LeadState, IRequest<Validation<Error, LeadState>>;

public class EditLeadCommandHandler : BaseCommandHandler<ApplicationContext, LeadState, EditLeadCommand>, IRequestHandler<EditLeadCommand, Validation<Error, LeadState>>
{
    public EditLeadCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLeadCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LeadState>> Handle(EditLeadCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditLead(request, cancellationToken));


	public async Task<Validation<Error, LeadState>> EditLead(EditLeadCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Lead.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateContactList(entity, request, cancellationToken);
		await UpdateContactPersonList(entity, request, cancellationToken);
		await UpdateOfferingHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, LeadState>(entity);
	}
	
	private async Task UpdateContactList(LeadState entity, EditLeadCommand request, CancellationToken cancellationToken)
	{
		IList<ContactState> contactListForDeletion = new List<ContactState>();
		var queryContactForDeletion = Context.Contact.Where(l => l.LeadID == request.Id).AsNoTracking();
		if (entity.ContactList?.Count > 0)
		{
			queryContactForDeletion = queryContactForDeletion.Where(l => !(entity.ContactList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		contactListForDeletion = await queryContactForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var contact in contactListForDeletion!)
		{
			Context.Entry(contact).State = EntityState.Deleted;
		}
		if (entity.ContactList?.Count > 0)
		{
			foreach (var contact in entity.ContactList.Where(l => !contactListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ContactState>(x => x.Id == contact.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(contact).State = EntityState.Added;
				}
				else
				{
					Context.Entry(contact).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateContactPersonList(LeadState entity, EditLeadCommand request, CancellationToken cancellationToken)
	{
		IList<ContactPersonState> contactPersonListForDeletion = new List<ContactPersonState>();
		var queryContactPersonForDeletion = Context.ContactPerson.Where(l => l.LeadId == request.Id).AsNoTracking();
		if (entity.ContactPersonList?.Count > 0)
		{
			queryContactPersonForDeletion = queryContactPersonForDeletion.Where(l => !(entity.ContactPersonList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		contactPersonListForDeletion = await queryContactPersonForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var contactPerson in contactPersonListForDeletion!)
		{
			Context.Entry(contactPerson).State = EntityState.Deleted;
		}
		if (entity.ContactPersonList?.Count > 0)
		{
			foreach (var contactPerson in entity.ContactPersonList.Where(l => !contactPersonListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ContactPersonState>(x => x.Id == contactPerson.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(contactPerson).State = EntityState.Added;
				}
				else
				{
					Context.Entry(contactPerson).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateOfferingHistoryList(LeadState entity, EditLeadCommand request, CancellationToken cancellationToken)
	{
		IList<OfferingHistoryState> offeringHistoryListForDeletion = new List<OfferingHistoryState>();
		var queryOfferingHistoryForDeletion = Context.OfferingHistory.Where(l => l.LeadID == request.Id).AsNoTracking();
		if (entity.OfferingHistoryList?.Count > 0)
		{
			queryOfferingHistoryForDeletion = queryOfferingHistoryForDeletion.Where(l => !(entity.OfferingHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		offeringHistoryListForDeletion = await queryOfferingHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var offeringHistory in offeringHistoryListForDeletion!)
		{
			Context.Entry(offeringHistory).State = EntityState.Deleted;
		}
		if (entity.OfferingHistoryList?.Count > 0)
		{
			foreach (var offeringHistory in entity.OfferingHistoryList.Where(l => !offeringHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<OfferingHistoryState>(x => x.Id == offeringHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(offeringHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(offeringHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditLeadCommandValidator : AbstractValidator<EditLeadCommand>
{
    readonly ApplicationContext _context;

    public EditLeadCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Lead with id {PropertyValue} does not exists");
        
    }
}
