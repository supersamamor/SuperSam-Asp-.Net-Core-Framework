using AutoMapper;
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
using CTI.Common.Identity.Abstractions;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record ApproveOfferingVersionCommand(string OfferingHistoryId) : IRequest<Validation<Error, OfferingState>>;

public class ApproveOfferingVersionCommandHandler : IRequestHandler<ApproveOfferingVersionCommand, Validation<Error, OfferingState>>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly CompositeValidator<ApproveOfferingVersionCommand> _validators;
    private readonly OfferingRepository _offeringRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    public ApproveOfferingVersionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<ApproveOfferingVersionCommand> validator,
                                    OfferingRepository offeringRepository,
                                    IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _mapper = mapper;
        _validators = validator;
        _offeringRepository = offeringRepository;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Validation<Error, OfferingState>> Handle(ApproveOfferingVersionCommand request, CancellationToken cancellationToken) =>
        await _validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await ApproveOffering(request, cancellationToken));


    public async Task<Validation<Error, OfferingState>> ApproveOffering(ApproveOfferingVersionCommand request, CancellationToken cancellationToken)
    {       
        var offeringHistory = await _context.OfferingHistory
             .Include(l => l.UnitOfferedHistoryList!).ThenInclude(l=>l.AnnualIncrementHistoryList)           
             .Where(e => e.Id == request.OfferingHistoryId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var entity = await _context.Offering.Where(l => l.Id == offeringHistory!.OfferingID).Include(l=>l.Project).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);
        await _offeringRepository.RemoveUnitOfferedList(entity.Id);   
        _mapper.Map(offeringHistory, entity);
        await _offeringRepository.AutoCalculateOfferSheetFields(entity);
        entity.SetOfferingHistoryId(request.OfferingHistoryId);
        _offeringRepository.AddUnitOfferedList(entity, request.OfferingHistoryId);
        entity.SignOffering(_authenticatedUser.UserId!);
        _context.Update(entity);
        await _offeringRepository.UpdateLeadLatestUpdateDate(entity.LeadID!);
        _ = await _context.SaveChangesAsync(cancellationToken);       
        return Success<Error, OfferingState>(entity);
    }
}

