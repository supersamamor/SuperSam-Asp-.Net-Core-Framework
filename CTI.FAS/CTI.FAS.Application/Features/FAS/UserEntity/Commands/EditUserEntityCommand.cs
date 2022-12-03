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

public record EditUserEntityCommand : UserEntityState, IRequest<Validation<Error, UserEntityState>>;

public class EditUserEntityCommandHandler : BaseCommandHandler<ApplicationContext, UserEntityState, EditUserEntityCommand>, IRequestHandler<EditUserEntityCommand, Validation<Error, UserEntityState>>
{
    public EditUserEntityCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUserEntityCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, UserEntityState>> Handle(EditUserEntityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditUserEntityCommandValidator : AbstractValidator<EditUserEntityCommand>
{
    readonly ApplicationContext _context;

    public EditUserEntityCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UserEntityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UserEntity with id {PropertyValue} does not exists");
        
    }
}
