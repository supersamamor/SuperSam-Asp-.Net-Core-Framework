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

namespace CTI.ELMS.Application.Features.ELMS.ClientFeedback.Commands;

public record DeleteClientFeedbackCommand : BaseCommand, IRequest<Validation<Error, ClientFeedbackState>>;

public class DeleteClientFeedbackCommandHandler : BaseCommandHandler<ApplicationContext, ClientFeedbackState, DeleteClientFeedbackCommand>, IRequestHandler<DeleteClientFeedbackCommand, Validation<Error, ClientFeedbackState>>
{
    public DeleteClientFeedbackCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteClientFeedbackCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ClientFeedbackState>> Handle(DeleteClientFeedbackCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteClientFeedbackCommandValidator : AbstractValidator<DeleteClientFeedbackCommand>
{
    readonly ApplicationContext _context;

    public DeleteClientFeedbackCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ClientFeedbackState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ClientFeedback with id {PropertyValue} does not exists");
    }
}
