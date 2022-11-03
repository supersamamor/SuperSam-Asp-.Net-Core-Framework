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

namespace CTI.ELMS.Application.Features.ELMS.OperationType.Commands;

public record DeleteOperationTypeCommand : BaseCommand, IRequest<Validation<Error, OperationTypeState>>;

public class DeleteOperationTypeCommandHandler : BaseCommandHandler<ApplicationContext, OperationTypeState, DeleteOperationTypeCommand>, IRequestHandler<DeleteOperationTypeCommand, Validation<Error, OperationTypeState>>
{
    public DeleteOperationTypeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteOperationTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OperationTypeState>> Handle(DeleteOperationTypeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteOperationTypeCommandValidator : AbstractValidator<DeleteOperationTypeCommand>
{
    readonly ApplicationContext _context;

    public DeleteOperationTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OperationTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OperationType with id {PropertyValue} does not exists");
    }
}
