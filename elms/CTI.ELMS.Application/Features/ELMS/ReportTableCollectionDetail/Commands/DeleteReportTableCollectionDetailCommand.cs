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

namespace CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Commands;

public record DeleteReportTableCollectionDetailCommand : BaseCommand, IRequest<Validation<Error, ReportTableCollectionDetailState>>;

public class DeleteReportTableCollectionDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableCollectionDetailState, DeleteReportTableCollectionDetailCommand>, IRequestHandler<DeleteReportTableCollectionDetailCommand, Validation<Error, ReportTableCollectionDetailState>>
{
    public DeleteReportTableCollectionDetailCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportTableCollectionDetailCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportTableCollectionDetailState>> Handle(DeleteReportTableCollectionDetailCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportTableCollectionDetailCommandValidator : AbstractValidator<DeleteReportTableCollectionDetailCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportTableCollectionDetailCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableCollectionDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableCollectionDetail with id {PropertyValue} does not exists");
    }
}
