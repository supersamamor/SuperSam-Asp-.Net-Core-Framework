using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportDetail.Commands;

public record EditReportDetailCommand : ReportDetailState, IRequest<Validation<Error, ReportDetailState>>;

public class EditReportDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportDetailState, EditReportDetailCommand>, IRequestHandler<EditReportDetailCommand, Validation<Error, ReportDetailState>>
{
    public EditReportDetailCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportDetailCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportDetailState>> Handle(EditReportDetailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportDetailCommandValidator : AbstractValidator<EditReportDetailCommand>
{
    readonly ApplicationContext _context;

    public EditReportDetailCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportDetail with id {PropertyValue} does not exists");
        
    }
}
