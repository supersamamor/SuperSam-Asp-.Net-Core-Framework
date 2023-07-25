using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Commands;

public record DeleteReportTableJoinParameterCommand : BaseCommand, IRequest<Validation<Error, ReportTableJoinParameterState>>;

public class DeleteReportTableJoinParameterCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableJoinParameterState, DeleteReportTableJoinParameterCommand>, IRequestHandler<DeleteReportTableJoinParameterCommand, Validation<Error, ReportTableJoinParameterState>>
{
    public DeleteReportTableJoinParameterCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportTableJoinParameterCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportTableJoinParameterState>> Handle(DeleteReportTableJoinParameterCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportTableJoinParameterCommandValidator : AbstractValidator<DeleteReportTableJoinParameterCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportTableJoinParameterCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableJoinParameterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableJoinParameter with id {PropertyValue} does not exists");
    }
}
