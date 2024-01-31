using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Commands;

public record DeleteReportCommand : BaseCommand, IRequest<Validation<Error, ReportState>>;

public class DeleteReportCommandHandler : BaseCommandHandler<ApplicationContext, ReportState, DeleteReportCommand>, IRequestHandler<DeleteReportCommand, Validation<Error, ReportState>>
{
    public DeleteReportCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportState>> Handle(DeleteReportCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Report with id {PropertyValue} does not exists");
    }
}
