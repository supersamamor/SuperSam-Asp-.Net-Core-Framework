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

public record AddReportDetailCommand : ReportDetailState, IRequest<Validation<Error, ReportDetailState>>;

public class AddReportDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportDetailState, AddReportDetailCommand>, IRequestHandler<AddReportDetailCommand, Validation<Error, ReportDetailState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportDetailCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportDetailCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportDetailState>> Handle(AddReportDetailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportDetailCommandValidator : AbstractValidator<AddReportDetailCommand>
{
    readonly ApplicationContext _context;

    public AddReportDetailCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportDetail with id {PropertyValue} already exists");
        
    }
}
