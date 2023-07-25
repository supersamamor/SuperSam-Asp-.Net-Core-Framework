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

namespace CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;

public record DeleteReportFilterGroupingCommand : BaseCommand, IRequest<Validation<Error, ReportFilterGroupingState>>;

public class DeleteReportFilterGroupingCommandHandler : BaseCommandHandler<ApplicationContext, ReportFilterGroupingState, DeleteReportFilterGroupingCommand>, IRequestHandler<DeleteReportFilterGroupingCommand, Validation<Error, ReportFilterGroupingState>>
{
    public DeleteReportFilterGroupingCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportFilterGroupingCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportFilterGroupingState>> Handle(DeleteReportFilterGroupingCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportFilterGroupingCommandValidator : AbstractValidator<DeleteReportFilterGroupingCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportFilterGroupingCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportFilterGroupingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportFilterGrouping with id {PropertyValue} does not exists");
    }
}
