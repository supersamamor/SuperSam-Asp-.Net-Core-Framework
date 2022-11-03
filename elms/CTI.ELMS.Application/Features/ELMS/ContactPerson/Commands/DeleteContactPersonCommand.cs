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

namespace CTI.ELMS.Application.Features.ELMS.ContactPerson.Commands;

public record DeleteContactPersonCommand : BaseCommand, IRequest<Validation<Error, ContactPersonState>>;

public class DeleteContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, ContactPersonState, DeleteContactPersonCommand>, IRequestHandler<DeleteContactPersonCommand, Validation<Error, ContactPersonState>>
{
    public DeleteContactPersonCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteContactPersonCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ContactPersonState>> Handle(DeleteContactPersonCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteContactPersonCommandValidator : AbstractValidator<DeleteContactPersonCommand>
{
    readonly ApplicationContext _context;

    public DeleteContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ContactPerson with id {PropertyValue} does not exists");
    }
}
