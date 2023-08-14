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
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTeamCommandValidator : AbstractValidator<AddTeamCommand>
{
    readonly ApplicationContext _context;

    public AddTeamCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TeamState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Team with id {PropertyValue} already exists");
        RuleFor(x => x.TeamCode).MustAsync(async (teamCode, cancellation) => await _context.NotExists<TeamState>(x => x.TeamCode == teamCode, cancellationToken: cancellation)).WithMessage("Team with teamCode {PropertyValue} already exists");
	RuleFor(x => x.TeamName).MustAsync(async (teamName, cancellation) => await _context.NotExists<TeamState>(x => x.TeamName == teamName, cancellationToken: cancellation)).WithMessage("Team with teamName {PropertyValue} already exists");
	
    }
}
