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

namespace CTI.ELMS.Application.Features.ELMS.Salutation.Commands;

public record EditSalutationCommand : SalutationState, IRequest<Validation<Error, SalutationState>>;

public class EditSalutationCommandHandler : BaseCommandHandler<ApplicationContext, SalutationState, EditSalutationCommand>, IRequestHandler<EditSalutationCommand, Validation<Error, SalutationState>>
{
    public EditSalutationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSalutationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SalutationState>> Handle(EditSalutationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditSalutation(request, cancellationToken));


	public async Task<Validation<Error, SalutationState>> EditSalutation(EditSalutationCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Salutation.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateContactPersonList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SalutationState>(entity);
	}
	
	private async Task UpdateContactPersonList(SalutationState entity, EditSalutationCommand request, CancellationToken cancellationToken)
	{
		IList<ContactPersonState> contactPersonListForDeletion = new List<ContactPersonState>();
		var queryContactPersonForDeletion = Context.ContactPerson.Where(l => l.SalutationID == request.Id).AsNoTracking();
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
	
}

public class EditSalutationCommandValidator : AbstractValidator<EditSalutationCommand>
{
    readonly ApplicationContext _context;

    public EditSalutationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SalutationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Salutation with id {PropertyValue} does not exists");
        RuleFor(x => x.SalutationDescription).MustAsync(async (request, salutationDescription, cancellation) => await _context.NotExists<SalutationState>(x => x.SalutationDescription == salutationDescription && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Salutation with salutationDescription {PropertyValue} already exists");
	
    }
}
