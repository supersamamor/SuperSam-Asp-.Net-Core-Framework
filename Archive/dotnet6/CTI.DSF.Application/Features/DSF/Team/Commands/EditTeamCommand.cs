using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Team.Commands;

public record EditTeamCommand : TeamState, IRequest<Validation<Error, TeamState>>;

public class EditTeamCommandHandler : BaseCommandHandler<ApplicationContext, TeamState, EditTeamCommand>, IRequestHandler<EditTeamCommand, Validation<Error, TeamState>>
{
    public EditTeamCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTeamCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TeamState>> Handle(EditTeamCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditTeam(request, cancellationToken));


	public async Task<Validation<Error, TeamState>> EditTeam(EditTeamCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Team.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTaskCompanyAssignmentList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TeamState>(entity);
	}
	
	private async Task UpdateTaskCompanyAssignmentList(TeamState entity, EditTeamCommand request, CancellationToken cancellationToken)
	{
		IList<TaskCompanyAssignmentState> taskCompanyAssignmentListForDeletion = new List<TaskCompanyAssignmentState>();
		var queryTaskCompanyAssignmentForDeletion = Context.TaskCompanyAssignment.Where(l => l.TeamId == request.Id).AsNoTracking();
		if (entity.TaskCompanyAssignmentList?.Count > 0)
		{
			queryTaskCompanyAssignmentForDeletion = queryTaskCompanyAssignmentForDeletion.Where(l => !(entity.TaskCompanyAssignmentList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		taskCompanyAssignmentListForDeletion = await queryTaskCompanyAssignmentForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var taskCompanyAssignment in taskCompanyAssignmentListForDeletion!)
		{
			Context.Entry(taskCompanyAssignment).State = EntityState.Deleted;
		}
		if (entity.TaskCompanyAssignmentList?.Count > 0)
		{
			foreach (var taskCompanyAssignment in entity.TaskCompanyAssignmentList.Where(l => !taskCompanyAssignmentListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TaskCompanyAssignmentState>(x => x.Id == taskCompanyAssignment.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(taskCompanyAssignment).State = EntityState.Added;
				}
				else
				{
					Context.Entry(taskCompanyAssignment).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditTeamCommandValidator : AbstractValidator<EditTeamCommand>
{
    readonly ApplicationContext _context;

    public EditTeamCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TeamState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Team with id {PropertyValue} does not exists");
        
    }
}
