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

namespace CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;

public record DeleteReportColumnDetailCommand : BaseCommand, IRequest<Validation<Error, ReportColumnDetailState>>;

public class DeleteReportColumnDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnDetailState, DeleteReportColumnDetailCommand>, IRequestHandler<DeleteReportColumnDetailCommand, Validation<Error, ReportColumnDetailState>>
{
    public DeleteReportColumnDetailCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportColumnDetailCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportColumnDetailState>> Handle(DeleteReportColumnDetailCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportColumnDetailCommandValidator : AbstractValidator<DeleteReportColumnDetailCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportColumnDetailCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportColumnDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnDetail with id {PropertyValue} does not exists");
    }
}
