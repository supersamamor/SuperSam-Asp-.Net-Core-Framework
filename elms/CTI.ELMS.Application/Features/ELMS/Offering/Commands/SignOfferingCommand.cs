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

public record SignOfferingCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class SignOfferingCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, SignOfferingCommand>, IRequestHandler<SignOfferingCommand, Validation<Error, OfferingState>>
{
    private readonly OfferingRepository _offeringRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    public SignOfferingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<SignOfferingCommand> validator,
                                     OfferingRepository offeringRepository,
                                     IAuthenticatedUser authenticatedUser) : base(context, mapper, validator)
    {
        _offeringRepository = offeringRepository;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Validation<Error, OfferingState>> Handle(SignOfferingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditOffering(request, cancellationToken));


	public async Task<Validation<Error, OfferingState>> EditOffering(SignOfferingCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Offering.Where(l => l.Id == request.Id)
            .Include(l=>l.Project).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);
        entity.SignOffering(_authenticatedUser.UserId!);
        Context.Update(entity);
        await _offeringRepository.UpdateLeadLatestUpdateDate(entity.LeadID!);
        _ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OfferingState>(entity);
	}    
}

public class SignOfferingCommandValidator : AbstractValidator<SignOfferingCommand>
{
    readonly ApplicationContext _context;

    public SignOfferingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} does not exists");
        
    }
}
