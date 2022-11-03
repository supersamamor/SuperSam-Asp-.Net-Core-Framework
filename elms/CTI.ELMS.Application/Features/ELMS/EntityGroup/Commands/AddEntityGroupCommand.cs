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

namespace CTI.ELMS.Application.Features.ELMS.EntityGroup.Commands;

public record AddEntityGroupCommand : EntityGroupState, IRequest<Validation<Error, EntityGroupState>>;

public class AddEntityGroupCommandHandler : BaseCommandHandler<ApplicationContext, EntityGroupState, AddEntityGroupCommand>, IRequestHandler<AddEntityGroupCommand, Validation<Error, EntityGroupState>>
{
	private readonly IdentityContext _identityContext;
    public AddEntityGroupCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddEntityGroupCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, EntityGroupState>> Handle(AddEntityGroupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddEntityGroupCommandValidator : AbstractValidator<AddEntityGroupCommand>
{
    readonly ApplicationContext _context;

    public AddEntityGroupCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<EntityGroupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EntityGroup with id {PropertyValue} already exists");
        
    }
}
