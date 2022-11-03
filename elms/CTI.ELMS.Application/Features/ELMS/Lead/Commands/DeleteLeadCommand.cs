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

namespace CTI.ELMS.Application.Features.ELMS.Lead.Commands;

public record DeleteLeadCommand : BaseCommand, IRequest<Validation<Error, LeadState>>;

public class DeleteLeadCommandHandler : BaseCommandHandler<ApplicationContext, LeadState, DeleteLeadCommand>, IRequestHandler<DeleteLeadCommand, Validation<Error, LeadState>>
{
    public DeleteLeadCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteLeadCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LeadState>> Handle(DeleteLeadCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLeadCommandValidator : AbstractValidator<DeleteLeadCommand>
{
    readonly ApplicationContext _context;

    public DeleteLeadCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Lead with id {PropertyValue} does not exists");
    }
}
