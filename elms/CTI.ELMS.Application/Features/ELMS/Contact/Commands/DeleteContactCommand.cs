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

namespace CTI.ELMS.Application.Features.ELMS.Contact.Commands;

public record DeleteContactCommand : BaseCommand, IRequest<Validation<Error, ContactState>>;

public class DeleteContactCommandHandler : BaseCommandHandler<ApplicationContext, ContactState, DeleteContactCommand>, IRequestHandler<DeleteContactCommand, Validation<Error, ContactState>>
{
    public DeleteContactCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteContactCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ContactState>> Handle(DeleteContactCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    readonly ApplicationContext _context;

    public DeleteContactCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ContactState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Contact with id {PropertyValue} does not exists");
    }
}
