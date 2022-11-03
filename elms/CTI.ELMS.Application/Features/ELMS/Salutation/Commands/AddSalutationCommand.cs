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

public record AddSalutationCommand : SalutationState, IRequest<Validation<Error, SalutationState>>;

public class AddSalutationCommandHandler : BaseCommandHandler<ApplicationContext, SalutationState, AddSalutationCommand>, IRequestHandler<AddSalutationCommand, Validation<Error, SalutationState>>
{
	private readonly IdentityContext _identityContext;
    public AddSalutationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSalutationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, SalutationState>> Handle(AddSalutationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddSalutation(request, cancellationToken));


	public async Task<Validation<Error, SalutationState>> AddSalutation(AddSalutationCommand request, CancellationToken cancellationToken)
	{
		SalutationState entity = Mapper.Map<SalutationState>(request);
		UpdateContactPersonList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SalutationState>(entity);
	}
	
	private void UpdateContactPersonList(SalutationState entity)
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

public class AddSalutationCommandValidator : AbstractValidator<AddSalutationCommand>
{
    readonly ApplicationContext _context;

    public AddSalutationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SalutationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Salutation with id {PropertyValue} already exists");
        RuleFor(x => x.SalutationDescription).MustAsync(async (salutationDescription, cancellation) => await _context.NotExists<SalutationState>(x => x.SalutationDescription == salutationDescription, cancellationToken: cancellation)).WithMessage("Salutation with salutationDescription {PropertyValue} already exists");
	
    }
}
