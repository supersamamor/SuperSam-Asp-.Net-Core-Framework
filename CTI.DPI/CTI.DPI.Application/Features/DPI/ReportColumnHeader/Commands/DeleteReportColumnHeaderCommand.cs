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

namespace CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;

public record DeleteReportColumnHeaderCommand : BaseCommand, IRequest<Validation<Error, ReportColumnHeaderState>>;

public class DeleteReportColumnHeaderCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnHeaderState, DeleteReportColumnHeaderCommand>, IRequestHandler<DeleteReportColumnHeaderCommand, Validation<Error, ReportColumnHeaderState>>
{
    public DeleteReportColumnHeaderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportColumnHeaderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportColumnHeaderState>> Handle(DeleteReportColumnHeaderCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportColumnHeaderCommandValidator : AbstractValidator<DeleteReportColumnHeaderCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportColumnHeaderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportColumnHeaderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnHeader with id {PropertyValue} does not exists");
    }
}
