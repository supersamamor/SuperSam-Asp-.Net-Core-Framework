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

namespace CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Commands;

public record DeleteLeadTouchPointCommand : BaseCommand, IRequest<Validation<Error, LeadTouchPointState>>;

public class DeleteLeadTouchPointCommandHandler : BaseCommandHandler<ApplicationContext, LeadTouchPointState, DeleteLeadTouchPointCommand>, IRequestHandler<DeleteLeadTouchPointCommand, Validation<Error, LeadTouchPointState>>
{
    public DeleteLeadTouchPointCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteLeadTouchPointCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LeadTouchPointState>> Handle(DeleteLeadTouchPointCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLeadTouchPointCommandValidator : AbstractValidator<DeleteLeadTouchPointCommand>
{
    readonly ApplicationContext _context;

    public DeleteLeadTouchPointCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTouchPointState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTouchPoint with id {PropertyValue} does not exists");
    }
}
