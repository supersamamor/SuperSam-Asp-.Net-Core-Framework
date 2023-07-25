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

namespace CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;

public record DeleteReportQueryFilterCommand : BaseCommand, IRequest<Validation<Error, ReportQueryFilterState>>;

public class DeleteReportQueryFilterCommandHandler : BaseCommandHandler<ApplicationContext, ReportQueryFilterState, DeleteReportQueryFilterCommand>, IRequestHandler<DeleteReportQueryFilterCommand, Validation<Error, ReportQueryFilterState>>
{
    public DeleteReportQueryFilterCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportQueryFilterCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportQueryFilterState>> Handle(DeleteReportQueryFilterCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportQueryFilterCommandValidator : AbstractValidator<DeleteReportQueryFilterCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportQueryFilterCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportQueryFilterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportQueryFilter with id {PropertyValue} does not exists");
    }
}
