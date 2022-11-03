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

namespace CTI.ELMS.Application.Features.ELMS.IFCAARAllocation.Commands;

public record DeleteIFCAARAllocationCommand : BaseCommand, IRequest<Validation<Error, IFCAARAllocationState>>;

public class DeleteIFCAARAllocationCommandHandler : BaseCommandHandler<ApplicationContext, IFCAARAllocationState, DeleteIFCAARAllocationCommand>, IRequestHandler<DeleteIFCAARAllocationCommand, Validation<Error, IFCAARAllocationState>>
{
    public DeleteIFCAARAllocationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteIFCAARAllocationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, IFCAARAllocationState>> Handle(DeleteIFCAARAllocationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteIFCAARAllocationCommandValidator : AbstractValidator<DeleteIFCAARAllocationCommand>
{
    readonly ApplicationContext _context;

    public DeleteIFCAARAllocationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCAARAllocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAARAllocation with id {PropertyValue} does not exists");
    }
}
