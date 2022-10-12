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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportDetail.Commands;

public record DeleteReportDetailCommand : BaseCommand, IRequest<Validation<Error, ReportDetailState>>;

public class DeleteReportDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportDetailState, DeleteReportDetailCommand>, IRequestHandler<DeleteReportDetailCommand, Validation<Error, ReportDetailState>>
{
    public DeleteReportDetailCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportDetailCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportDetailState>> Handle(DeleteReportDetailCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportDetailCommandValidator : AbstractValidator<DeleteReportDetailCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportDetailCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportDetail with id {PropertyValue} does not exists");
    }
}
