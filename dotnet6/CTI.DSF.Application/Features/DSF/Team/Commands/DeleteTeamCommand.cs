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

namespace CTI.DSF.Application.Features.DSF.Team.Commands;

public record DeleteTeamCommand : BaseCommand, IRequest<Validation<Error, TeamState>>;

public class DeleteTeamCommandHandler : BaseCommandHandler<ApplicationContext, TeamState, DeleteTeamCommand>, IRequestHandler<DeleteTeamCommand, Validation<Error, TeamState>>
{
    public DeleteTeamCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTeamCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TeamState>> Handle(DeleteTeamCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
{
    readonly ApplicationContext _context;

    public DeleteTeamCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TeamState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Team with id {PropertyValue} does not exists");
    }
}
