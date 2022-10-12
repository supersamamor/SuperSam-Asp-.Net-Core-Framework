using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Commands;

public record DeleteReportInboxCommand : BaseCommand, IRequest<Validation<Error, ReportInboxState>>;

public class DeleteReportInboxCommandHandler : BaseCommandHandler<ApplicationContext, ReportInboxState, DeleteReportInboxCommand>, IRequestHandler<DeleteReportInboxCommand, Validation<Error, ReportInboxState>>
{
    public DeleteReportInboxCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportInboxCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportInboxState>> Handle(DeleteReportInboxCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportInboxCommandValidator : AbstractValidator<DeleteReportInboxCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportInboxCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportInboxState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportInbox with id {PropertyValue} does not exists");
    }
}
