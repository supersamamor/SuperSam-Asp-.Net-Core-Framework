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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Commands;

public record EditReportTableJoinParameterCommand : ReportTableJoinParameterState, IRequest<Validation<Error, ReportTableJoinParameterState>>;

public class EditReportTableJoinParameterCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableJoinParameterState, EditReportTableJoinParameterCommand>, IRequestHandler<EditReportTableJoinParameterCommand, Validation<Error, ReportTableJoinParameterState>>
{
    public EditReportTableJoinParameterCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportTableJoinParameterCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportTableJoinParameterState>> Handle(EditReportTableJoinParameterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportTableJoinParameterCommandValidator : AbstractValidator<EditReportTableJoinParameterCommand>
{
    readonly ApplicationContext _context;

    public EditReportTableJoinParameterCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableJoinParameterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableJoinParameter with id {PropertyValue} does not exists");
        
    }
}
