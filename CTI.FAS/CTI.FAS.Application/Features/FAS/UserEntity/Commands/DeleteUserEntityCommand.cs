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

namespace CTI.FAS.Application.Features.FAS.UserEntity.Commands;

public record DeleteUserEntityCommand : BaseCommand, IRequest<Validation<Error, UserEntityState>>;

public class DeleteUserEntityCommandHandler : BaseCommandHandler<ApplicationContext, UserEntityState, DeleteUserEntityCommand>, IRequestHandler<DeleteUserEntityCommand, Validation<Error, UserEntityState>>
{
    public DeleteUserEntityCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUserEntityCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UserEntityState>> Handle(DeleteUserEntityCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUserEntityCommandValidator : AbstractValidator<DeleteUserEntityCommand>
{
    readonly ApplicationContext _context;

    public DeleteUserEntityCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UserEntityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UserEntity with id {PropertyValue} does not exists");
    }
}
