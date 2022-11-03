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

namespace CTI.ELMS.Application.Features.ELMS.EntityGroup.Commands;

public record DeleteEntityGroupCommand : BaseCommand, IRequest<Validation<Error, EntityGroupState>>;

public class DeleteEntityGroupCommandHandler : BaseCommandHandler<ApplicationContext, EntityGroupState, DeleteEntityGroupCommand>, IRequestHandler<DeleteEntityGroupCommand, Validation<Error, EntityGroupState>>
{
    public DeleteEntityGroupCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteEntityGroupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, EntityGroupState>> Handle(DeleteEntityGroupCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteEntityGroupCommandValidator : AbstractValidator<DeleteEntityGroupCommand>
{
    readonly ApplicationContext _context;

    public DeleteEntityGroupCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EntityGroupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EntityGroup with id {PropertyValue} does not exists");
    }
}
