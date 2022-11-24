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
using System.Data;
using CTI.ELMS.Application.Repositories;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record NewOfferingVersionCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class NewOfferingVersionCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, NewOfferingVersionCommand>, IRequestHandler<NewOfferingVersionCommand, Validation<Error, OfferingState>>
{
    private readonly OfferingRepository _offeringRepository;
    public NewOfferingVersionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<NewOfferingVersionCommand> validator,                           
                                    OfferingRepository offeringRepository) : base(context, mapper, validator)
    {   
        _offeringRepository = offeringRepository;
    }

    public async Task<Validation<Error, OfferingState>> Handle(NewOfferingVersionCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await AddOffering(request, cancellationToken));


    public async Task<Validation<Error, OfferingState>> AddOffering(NewOfferingVersionCommand request, CancellationToken cancellationToken)
    {
        var entity = await Context.Offering.Where(l => l.Id == request.Id).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);
        Mapper.Map(request, entity);
        await _offeringRepository.RemovePreSelectedUnitList(entity.Id); 
        await _offeringRepository.RemoveUnitOfferedList(entity.Id);   
        _offeringRepository.AddPreSelectedUnitList(entity);     
        await _offeringRepository.AutoCalculateOfferSheetFields(entity);
        var offeringHistoryId = await _offeringRepository.AddOfferingHistory(entity);
        _offeringRepository.AddUnitOfferedList(entity, offeringHistoryId);
        Context.Update(entity);  
        await _offeringRepository.UpdateLeadLatestUpdateDate(entity.LeadID!);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, OfferingState>(entity);
    }    
}

public class NewOfferingVersionCommandValidator : AbstractValidator<NewOfferingVersionCommand>
{
    readonly ApplicationContext _context;

    public NewOfferingVersionCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} does not exists");
    }
}
