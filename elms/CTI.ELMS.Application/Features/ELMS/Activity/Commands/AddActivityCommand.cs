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

namespace CTI.ELMS.Application.Features.ELMS.Activity.Commands;

public record AddActivityCommand : ActivityState, IRequest<Validation<Error, ActivityState>>;

public class AddActivityCommandHandler : BaseCommandHandler<ApplicationContext, ActivityState, AddActivityCommand>, IRequestHandler<AddActivityCommand, Validation<Error, ActivityState>>
{
    public AddActivityCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddActivityCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ActivityState>> Handle(AddActivityCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await AddActivity(request, cancellationToken));


    public async Task<Validation<Error, ActivityState>> AddActivity(AddActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await GetActivityByLeadProject(request.LeadID!, request.ProjectID!);       
        if (entity != null)
        {
            Mapper.Map(request, entity);
            await UpdateUnitActivityList(entity, request, cancellationToken);
            Context.Entry(entity).State = EntityState.Modified;
        }
        else
        {
            entity = Mapper.Map<ActivityState>(request);         
            AddUnitActivityList(entity);
            var nextStepPCT = await GetLeadTaskNextStepItem(request.LeadTaskId!, request.ClientFeedbackId!, request.NextStepId!);
            if (nextStepPCT != null && nextStepPCT.PCTDay != 0)
            {
                if (entity.ActivityDate != null)
                { 
                    entity.SetPCTDate(((DateTime)entity.ActivityDate).AddDays((int)nextStepPCT!.PCTDay!)); 
                }                
            }            
            _ = await Context.AddAsync(entity, cancellationToken);
        }
        AddActivityHistory(entity);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, ActivityState>(entity);
    }
    private void AddActivityHistory(ActivityState entity)
    {     
        var activityHistory = Mapper.Map<ActivityHistoryState>(entity);
        Context.Entry(activityHistory).State = EntityState.Added;
    }
    private void AddUnitActivityList(ActivityState entity)
    {
        if (entity.UnitActivityList?.Count > 0)
        {
            foreach (var unitActivity in entity.UnitActivityList!)
            {
                Context.Entry(unitActivity).State = EntityState.Added;
            }
        }
    }
    private async Task UpdateUnitActivityList(ActivityState entity, AddActivityCommand request, CancellationToken cancellationToken)
    {
        IList<UnitActivityState> unitActivityListForDeletion = new List<UnitActivityState>();
        var queryUnitActivityForDeletion = Context.UnitActivity.Where(l => l.ActivityID == request.Id).AsNoTracking();
        if (entity.UnitActivityList?.Count > 0)
        {
            queryUnitActivityForDeletion = queryUnitActivityForDeletion.Where(l => !(entity.UnitActivityList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        unitActivityListForDeletion = await queryUnitActivityForDeletion.ToListAsync(cancellationToken: cancellationToken);
        foreach (var unitActivity in unitActivityListForDeletion!)
        {
            Context.Entry(unitActivity).State = EntityState.Deleted;
        }
        if (entity.UnitActivityList?.Count > 0)
        {
            foreach (var unitActivity in entity.UnitActivityList.Where(l => !unitActivityListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await Context.NotExists<UnitActivityState>(x => x.Id == unitActivity.Id, cancellationToken: cancellationToken))
                {
                    Context.Entry(unitActivity).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(unitActivity).State = EntityState.Modified;
                }
            }
        }
    }
    public async Task<ActivityState?> GetActivityByLeadProject(string leadID, string projectID)
    {
        return await Context.Activity.         
            FirstOrDefaultAsync(i => i.LeadID == leadID && i.ProjectID == projectID);
    }
    public async Task<LeadTaskNextStepState?> GetLeadTaskNextStepItem(string ladTaskId, string clientFeedbackId, string nextStepId)
    {
        return await (from ln in Context.LeadTaskNextStep
                      where ln.LeadTaskId == ladTaskId && ln.ClientFeedbackId == clientFeedbackId && ln.NextStepId == nextStepId
                      select ln).Distinct().OrderBy(l => l.ClientFeedbackId).AsNoTracking().FirstOrDefaultAsync();
    }
}

public class AddActivityCommandValidator : AbstractValidator<AddActivityCommand>
{ 
}
