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

namespace CTI.ELMS.Application.Features.ELMS.Project.Commands;

public record EditProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class EditProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, EditProjectCommand>, IRequestHandler<EditProjectCommand, Validation<Error, ProjectState>>
{
    public EditProjectCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(EditProjectCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditProject(request, cancellationToken));


	public async Task<Validation<Error, ProjectState>> EditProject(EditProjectCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Project.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateUserProjectAssignmentList(entity, request, cancellationToken);
		await UpdateOfferingHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectState>(entity);
	}
	
	private async Task UpdateUserProjectAssignmentList(ProjectState entity, EditProjectCommand request, CancellationToken cancellationToken)
	{
		IList<UserProjectAssignmentState> userProjectAssignmentListForDeletion = new List<UserProjectAssignmentState>();
		var queryUserProjectAssignmentForDeletion = Context.UserProjectAssignment.Where(l => l.ProjectID == request.Id).AsNoTracking();
		if (entity.UserProjectAssignmentList?.Count > 0)
		{
			queryUserProjectAssignmentForDeletion = queryUserProjectAssignmentForDeletion.Where(l => !(entity.UserProjectAssignmentList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		userProjectAssignmentListForDeletion = await queryUserProjectAssignmentForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var userProjectAssignment in userProjectAssignmentListForDeletion!)
		{
			Context.Entry(userProjectAssignment).State = EntityState.Deleted;
		}
		if (entity.UserProjectAssignmentList?.Count > 0)
		{
			foreach (var userProjectAssignment in entity.UserProjectAssignmentList.Where(l => !userProjectAssignmentListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<UserProjectAssignmentState>(x => x.Id == userProjectAssignment.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(userProjectAssignment).State = EntityState.Added;
				}
				else
				{
					Context.Entry(userProjectAssignment).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateOfferingHistoryList(ProjectState entity, EditProjectCommand request, CancellationToken cancellationToken)
	{
		IList<OfferingHistoryState> offeringHistoryListForDeletion = new List<OfferingHistoryState>();
		var queryOfferingHistoryForDeletion = Context.OfferingHistory.Where(l => l.ProjectID == request.Id).AsNoTracking();
		if (entity.OfferingHistoryList?.Count > 0)
		{
			queryOfferingHistoryForDeletion = queryOfferingHistoryForDeletion.Where(l => !(entity.OfferingHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		offeringHistoryListForDeletion = await queryOfferingHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var offeringHistory in offeringHistoryListForDeletion!)
		{
			Context.Entry(offeringHistory).State = EntityState.Deleted;
		}
		if (entity.OfferingHistoryList?.Count > 0)
		{
			foreach (var offeringHistory in entity.OfferingHistoryList.Where(l => !offeringHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<OfferingHistoryState>(x => x.Id == offeringHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(offeringHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(offeringHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditProjectCommandValidator : AbstractValidator<EditProjectCommand>
{
    readonly ApplicationContext _context;

    public EditProjectCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Project with id {PropertyValue} does not exists");
        
    }
}
