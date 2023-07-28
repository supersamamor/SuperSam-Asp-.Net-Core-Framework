using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Commands;

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
