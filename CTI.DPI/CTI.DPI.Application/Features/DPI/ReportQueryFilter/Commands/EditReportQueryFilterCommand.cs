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

namespace CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;

public record EditReportQueryFilterCommand : ReportQueryFilterState, IRequest<Validation<Error, ReportQueryFilterState>>;

public class EditReportQueryFilterCommandHandler : BaseCommandHandler<ApplicationContext, ReportQueryFilterState, EditReportQueryFilterCommand>, IRequestHandler<EditReportQueryFilterCommand, Validation<Error, ReportQueryFilterState>>
{
    public EditReportQueryFilterCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportQueryFilterCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportQueryFilterState>> Handle(EditReportQueryFilterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportQueryFilterCommandValidator : AbstractValidator<EditReportQueryFilterCommand>
{
    readonly ApplicationContext _context;

    public EditReportQueryFilterCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportQueryFilterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportQueryFilter with id {PropertyValue} does not exists");
        
    }
}
