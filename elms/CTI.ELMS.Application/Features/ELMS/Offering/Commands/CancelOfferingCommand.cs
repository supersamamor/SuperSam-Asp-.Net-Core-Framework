using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Application.Repositories;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record CancelOfferingCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class CancelOfferingCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, CancelOfferingCommand>, IRequestHandler<CancelOfferingCommand, Validation<Error, OfferingState>>
{
    private readonly OfferingRepository _offeringRepository;
    public CancelOfferingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<CancelOfferingCommand> validator,
                                     OfferingRepository offeringRepository) : base(context, mapper, validator)
    {
        _offeringRepository = offeringRepository;
    }

    public async Task<Validation<Error, OfferingState>> Handle(CancelOfferingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await CancelOffering(request, cancellationToken));


	public async Task<Validation<Error, OfferingState>> CancelOffering(CancelOfferingCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Offering.Where(l => l.Id == request.Id)
            .Include(l=>l.Project).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);
        entity.CancelOffering();
        Context.Update(entity);
        await _offeringRepository.UpdateLeadLatestUpdateDate(entity.LeadID!);
        _ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OfferingState>(entity);
	}    
}

public class CancelOfferingCommandValidator : AbstractValidator<CancelOfferingCommand>
{
    readonly ApplicationContext _context;

    public CancelOfferingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} does not exists");
        
    }
}
