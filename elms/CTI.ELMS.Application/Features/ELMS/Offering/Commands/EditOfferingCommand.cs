using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
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

public record EditOfferingCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class EditOfferingCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, EditOfferingCommand>, IRequestHandler<EditOfferingCommand, Validation<Error, OfferingState>>
{
    private readonly OfferingRepository _offeringRepository;
    public EditOfferingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditOfferingCommand> validator,
                                     OfferingRepository offeringRepository) : base(context, mapper, validator)
    {
        _offeringRepository = offeringRepository;
    }

    public async Task<Validation<Error, OfferingState>> Handle(EditOfferingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditOffering(request, cancellationToken));


	public async Task<Validation<Error, OfferingState>> EditOffering(EditOfferingCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Offering.Where(l => l.Id == request.Id).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
        Context.Update(entity);
        await _offeringRepository.UpdateOfferingHistory(entity);
        _ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OfferingState>(entity);
	}    
}

public class EditOfferingCommandValidator : AbstractValidator<EditOfferingCommand>
{
    readonly ApplicationContext _context;

    public EditOfferingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} does not exists");
        
    }
}
