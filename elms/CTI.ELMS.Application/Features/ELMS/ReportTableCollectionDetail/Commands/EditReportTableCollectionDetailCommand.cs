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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Commands;

public record EditReportTableCollectionDetailCommand : ReportTableCollectionDetailState, IRequest<Validation<Error, ReportTableCollectionDetailState>>;

public class EditReportTableCollectionDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableCollectionDetailState, EditReportTableCollectionDetailCommand>, IRequestHandler<EditReportTableCollectionDetailCommand, Validation<Error, ReportTableCollectionDetailState>>
{
    public EditReportTableCollectionDetailCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportTableCollectionDetailCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportTableCollectionDetailState>> Handle(EditReportTableCollectionDetailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportTableCollectionDetailCommandValidator : AbstractValidator<EditReportTableCollectionDetailCommand>
{
    readonly ApplicationContext _context;

    public EditReportTableCollectionDetailCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableCollectionDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableCollectionDetail with id {PropertyValue} does not exists");
        
    }
}
