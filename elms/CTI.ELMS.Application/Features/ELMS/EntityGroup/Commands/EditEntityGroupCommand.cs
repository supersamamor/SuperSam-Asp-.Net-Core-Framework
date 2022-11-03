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

public record EditEntityGroupCommand : EntityGroupState, IRequest<Validation<Error, EntityGroupState>>;

public class EditEntityGroupCommandHandler : BaseCommandHandler<ApplicationContext, EntityGroupState, EditEntityGroupCommand>, IRequestHandler<EditEntityGroupCommand, Validation<Error, EntityGroupState>>
{
    public EditEntityGroupCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditEntityGroupCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, EntityGroupState>> Handle(EditEntityGroupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditEntityGroupCommandValidator : AbstractValidator<EditEntityGroupCommand>
{
    readonly ApplicationContext _context;

    public EditEntityGroupCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EntityGroupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EntityGroup with id {PropertyValue} does not exists");
        
    }
}
