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

public record AddReportTableCollectionDetailCommand : ReportTableCollectionDetailState, IRequest<Validation<Error, ReportTableCollectionDetailState>>;

public class AddReportTableCollectionDetailCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableCollectionDetailState, AddReportTableCollectionDetailCommand>, IRequestHandler<AddReportTableCollectionDetailCommand, Validation<Error, ReportTableCollectionDetailState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportTableCollectionDetailCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportTableCollectionDetailCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportTableCollectionDetailState>> Handle(AddReportTableCollectionDetailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportTableCollectionDetailCommandValidator : AbstractValidator<AddReportTableCollectionDetailCommand>
{
    readonly ApplicationContext _context;

    public AddReportTableCollectionDetailCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportTableCollectionDetailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableCollectionDetail with id {PropertyValue} already exists");
        
    }
}
