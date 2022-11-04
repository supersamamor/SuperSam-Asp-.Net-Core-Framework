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

namespace CTI.ELMS.Application.Features.ELMS.ClientFeedback.Commands;

public record EditClientFeedbackCommand : ClientFeedbackState, IRequest<Validation<Error, ClientFeedbackState>>;

public class EditClientFeedbackCommandHandler : BaseCommandHandler<ApplicationContext, ClientFeedbackState, EditClientFeedbackCommand>, IRequestHandler<EditClientFeedbackCommand, Validation<Error, ClientFeedbackState>>
{
    public EditClientFeedbackCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditClientFeedbackCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ClientFeedbackState>> Handle(EditClientFeedbackCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditClientFeedback(request, cancellationToken));


	public async Task<Validation<Error, ClientFeedbackState>> EditClientFeedback(EditClientFeedbackCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ClientFeedback.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateLeadTaskClientFeedBackList(entity, request, cancellationToken);
		await UpdateLeadTaskNextStepList(entity, request, cancellationToken);
		await UpdateActivityHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ClientFeedbackState>(entity);
	}
	
	private async Task UpdateLeadTaskClientFeedBackList(ClientFeedbackState entity, EditClientFeedbackCommand request, CancellationToken cancellationToken)
	{
		IList<LeadTaskClientFeedBackState> leadTaskClientFeedBackListForDeletion = new List<LeadTaskClientFeedBackState>();
		var queryLeadTaskClientFeedBackForDeletion = Context.LeadTaskClientFeedBack.Where(l => l.ClientFeedbackId == request.Id).AsNoTracking();
		if (entity.LeadTaskClientFeedBackList?.Count > 0)
		{
			queryLeadTaskClientFeedBackForDeletion = queryLeadTaskClientFeedBackForDeletion.Where(l => !(entity.LeadTaskClientFeedBackList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		leadTaskClientFeedBackListForDeletion = await queryLeadTaskClientFeedBackForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var leadTaskClientFeedBack in leadTaskClientFeedBackListForDeletion!)
		{
			Context.Entry(leadTaskClientFeedBack).State = EntityState.Deleted;
		}
		if (entity.LeadTaskClientFeedBackList?.Count > 0)
		{
			foreach (var leadTaskClientFeedBack in entity.LeadTaskClientFeedBackList.Where(l => !leadTaskClientFeedBackListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<LeadTaskClientFeedBackState>(x => x.Id == leadTaskClientFeedBack.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(leadTaskClientFeedBack).State = EntityState.Added;
				}
				else
				{
					Context.Entry(leadTaskClientFeedBack).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateLeadTaskNextStepList(ClientFeedbackState entity, EditClientFeedbackCommand request, CancellationToken cancellationToken)
	{
		IList<LeadTaskNextStepState> leadTaskNextStepListForDeletion = new List<LeadTaskNextStepState>();
		var queryLeadTaskNextStepForDeletion = Context.LeadTaskNextStep.Where(l => l.ClientFeedbackId == request.Id).AsNoTracking();
		if (entity.LeadTaskNextStepList?.Count > 0)
		{
			queryLeadTaskNextStepForDeletion = queryLeadTaskNextStepForDeletion.Where(l => !(entity.LeadTaskNextStepList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		leadTaskNextStepListForDeletion = await queryLeadTaskNextStepForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var leadTaskNextStep in leadTaskNextStepListForDeletion!)
		{
			Context.Entry(leadTaskNextStep).State = EntityState.Deleted;
		}
		if (entity.LeadTaskNextStepList?.Count > 0)
		{
			foreach (var leadTaskNextStep in entity.LeadTaskNextStepList.Where(l => !leadTaskNextStepListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<LeadTaskNextStepState>(x => x.Id == leadTaskNextStep.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(leadTaskNextStep).State = EntityState.Added;
				}
				else
				{
					Context.Entry(leadTaskNextStep).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateActivityHistoryList(ClientFeedbackState entity, EditClientFeedbackCommand request, CancellationToken cancellationToken)
	{
		IList<ActivityHistoryState> activityHistoryListForDeletion = new List<ActivityHistoryState>();
		var queryActivityHistoryForDeletion = Context.ActivityHistory.Where(l => l.ClientFeedbackId == request.Id).AsNoTracking();
		if (entity.ActivityHistoryList?.Count > 0)
		{
			queryActivityHistoryForDeletion = queryActivityHistoryForDeletion.Where(l => !(entity.ActivityHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		activityHistoryListForDeletion = await queryActivityHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var activityHistory in activityHistoryListForDeletion!)
		{
			Context.Entry(activityHistory).State = EntityState.Deleted;
		}
		if (entity.ActivityHistoryList?.Count > 0)
		{
			foreach (var activityHistory in entity.ActivityHistoryList.Where(l => !activityHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ActivityHistoryState>(x => x.Id == activityHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(activityHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(activityHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditClientFeedbackCommandValidator : AbstractValidator<EditClientFeedbackCommand>
{
    readonly ApplicationContext _context;

    public EditClientFeedbackCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ClientFeedbackState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ClientFeedback with id {PropertyValue} does not exists");
        RuleFor(x => x.ClientFeedbackName).MustAsync(async (request, clientFeedbackName, cancellation) => await _context.NotExists<ClientFeedbackState>(x => x.ClientFeedbackName == clientFeedbackName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ClientFeedback with clientFeedbackName {PropertyValue} already exists");
	
    }
}
