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

namespace CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;

public record EditReportColumnDetailCommand : ReportColumnDetailState, IRequest<Validation<Error, ReportColumnDetailState>>;

public class EditReportColumnDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnDetailState, EditReportColumnDetailCommand>, IRequestHandler<EditReportColumnDetailCommand, Validation<Error, ReportColumnDetailState>>
{
    public EditReportColumnDetailCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportColumnDetailCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportColumnDetailState>> Handle(EditReportColumnDetailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportColumnDetailCommandValidator : AbstractValidator<EditReportColumnDetailCommand>
{
    readonly ApplicationContext _context;

    public EditReportColumnDetailCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportColumnDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnDetail with id {PropertyValue} does not exists");
        
    }
}
