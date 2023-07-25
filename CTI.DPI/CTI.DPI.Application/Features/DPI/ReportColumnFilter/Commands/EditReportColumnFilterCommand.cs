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

namespace CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;

public record EditReportColumnFilterCommand : ReportColumnFilterState, IRequest<Validation<Error, ReportColumnFilterState>>;

public class EditReportColumnFilterCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnFilterState, EditReportColumnFilterCommand>, IRequestHandler<EditReportColumnFilterCommand, Validation<Error, ReportColumnFilterState>>
{
    public EditReportColumnFilterCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportColumnFilterCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportColumnFilterState>> Handle(EditReportColumnFilterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportColumnFilterCommandValidator : AbstractValidator<EditReportColumnFilterCommand>
{
    readonly ApplicationContext _context;

    public EditReportColumnFilterCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportColumnFilterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnFilter with id {PropertyValue} does not exists");
        
    }
}
