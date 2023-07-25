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

namespace CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;

public record DeleteReportColumnFilterCommand : BaseCommand, IRequest<Validation<Error, ReportColumnFilterState>>;

public class DeleteReportColumnFilterCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnFilterState, DeleteReportColumnFilterCommand>, IRequestHandler<DeleteReportColumnFilterCommand, Validation<Error, ReportColumnFilterState>>
{
    public DeleteReportColumnFilterCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportColumnFilterCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportColumnFilterState>> Handle(DeleteReportColumnFilterCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportColumnFilterCommandValidator : AbstractValidator<DeleteReportColumnFilterCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportColumnFilterCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportColumnFilterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnFilter with id {PropertyValue} does not exists");
    }
}
