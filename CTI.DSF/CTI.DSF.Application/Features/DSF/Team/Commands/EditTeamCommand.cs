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
			async request => await Edit(request, cancellationToken));
	
	
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
