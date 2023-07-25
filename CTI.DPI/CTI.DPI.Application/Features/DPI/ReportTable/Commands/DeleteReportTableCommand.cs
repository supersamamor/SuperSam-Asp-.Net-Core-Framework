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

namespace CTI.DPI.Application.Features.DPI.ReportTable.Commands;

public record DeleteReportTableCommand : BaseCommand, IRequest<Validation<Error, ReportTableState>>;

public class DeleteReportTableCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableState, DeleteReportTableCommand>, IRequestHandler<DeleteReportTableCommand, Validation<Error, ReportTableState>>
{
    public DeleteReportTableCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportTableCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportTableState>> Handle(DeleteReportTableCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportTableCommandValidator : AbstractValidator<DeleteReportTableCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportTableCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTable with id {PropertyValue} does not exists");
    }
}
