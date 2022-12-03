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

namespace CTI.FAS.Application.Features.FAS.Batch.Commands;

public record DeleteBatchCommand : BaseCommand, IRequest<Validation<Error, BatchState>>;

public class DeleteBatchCommandHandler : BaseCommandHandler<ApplicationContext, BatchState, DeleteBatchCommand>, IRequestHandler<DeleteBatchCommand, Validation<Error, BatchState>>
{
    public DeleteBatchCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBatchCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BatchState>> Handle(DeleteBatchCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBatchCommandValidator : AbstractValidator<DeleteBatchCommand>
{
    readonly ApplicationContext _context;

    public DeleteBatchCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BatchState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Batch with id {PropertyValue} does not exists");
    }
}
