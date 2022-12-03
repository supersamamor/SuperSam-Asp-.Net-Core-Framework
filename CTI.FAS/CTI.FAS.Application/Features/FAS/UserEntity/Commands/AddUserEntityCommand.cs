using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.UserEntity.Commands;

public record AddUserEntityCommand : UserEntityState, IRequest<Validation<Error, UserEntityState>>;

public class AddUserEntityCommandHandler : BaseCommandHandler<ApplicationContext, UserEntityState, AddUserEntityCommand>, IRequestHandler<AddUserEntityCommand, Validation<Error, UserEntityState>>
{
	private readonly IdentityContext _identityContext;
    public AddUserEntityCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUserEntityCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UserEntityState>> Handle(AddUserEntityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddUserEntityCommandValidator : AbstractValidator<AddUserEntityCommand>
{
    readonly ApplicationContext _context;

    public AddUserEntityCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UserEntityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UserEntity with id {PropertyValue} already exists");
        
    }
}
