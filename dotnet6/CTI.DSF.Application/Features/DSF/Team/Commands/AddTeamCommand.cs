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

public record AddTeamCommand : TeamState, IRequest<Validation<Error, TeamState>>;

public class AddTeamCommandHandler : BaseCommandHandler<ApplicationContext, TeamState, AddTeamCommand>, IRequestHandler<AddTeamCommand, Validation<Error, TeamState>>
{
	private readonly IdentityContext _identityContext;
    public AddTeamCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTeamCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, TeamState>> Handle(AddTeamCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddTeam(request, cancellationToken));


	public async Task<Validation<Error, TeamState>> AddTeam(AddTeamCommand request, CancellationToken cancellationToken)
	{
		TeamState entity = Mapper.Map<TeamState>(request);
		UpdateTaskCompanyAssignmentList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TeamState>(entity);
	}
	
	private void UpdateTaskCompanyAssignmentList(TeamState entity)
	{
		if (entity.TaskCompanyAssignmentList?.Count > 0)
		{
			foreach (var taskCompanyAssignment in entity.TaskCompanyAssignmentList!)
			{
				Context.Entry(taskCompanyAssignment).State = EntityState.Added;
			}
		}
	}
	
}

public class AddTeamCommandValidator : AbstractValidator<AddTeamCommand>
{
    readonly ApplicationContext _context;

    public AddTeamCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TeamState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Team with id {PropertyValue} already exists");
        
    }
}
