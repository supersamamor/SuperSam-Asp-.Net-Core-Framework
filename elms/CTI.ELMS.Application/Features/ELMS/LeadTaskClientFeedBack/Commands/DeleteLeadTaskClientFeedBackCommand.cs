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

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Commands;

public record DeleteLeadTaskClientFeedBackCommand : BaseCommand, IRequest<Validation<Error, LeadTaskClientFeedBackState>>;

public class DeleteLeadTaskClientFeedBackCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskClientFeedBackState, DeleteLeadTaskClientFeedBackCommand>, IRequestHandler<DeleteLeadTaskClientFeedBackCommand, Validation<Error, LeadTaskClientFeedBackState>>
{
    public DeleteLeadTaskClientFeedBackCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteLeadTaskClientFeedBackCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LeadTaskClientFeedBackState>> Handle(DeleteLeadTaskClientFeedBackCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLeadTaskClientFeedBackCommandValidator : AbstractValidator<DeleteLeadTaskClientFeedBackCommand>
{
    readonly ApplicationContext _context;

    public DeleteLeadTaskClientFeedBackCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTaskClientFeedBackState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTaskClientFeedBack with id {PropertyValue} does not exists");
    }
}
