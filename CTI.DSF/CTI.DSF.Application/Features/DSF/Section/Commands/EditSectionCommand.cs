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

namespace CTI.DSF.Application.Features.DSF.Section.Commands;

public record EditSectionCommand : SectionState, IRequest<Validation<Error, SectionState>>;

public class EditSectionCommandHandler : BaseCommandHandler<ApplicationContext, SectionState, EditSectionCommand>, IRequestHandler<EditSectionCommand, Validation<Error, SectionState>>
{
    public EditSectionCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSectionCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SectionState>> Handle(EditSectionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditSection(request, cancellationToken));


	public async Task<Validation<Error, SectionState>> EditSection(EditSectionCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Section.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTeamList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SectionState>(entity);
	}
	
	private async Task UpdateTeamList(SectionState entity, EditSectionCommand request, CancellationToken cancellationToken)
	{
		IList<TeamState> teamListForDeletion = new List<TeamState>();
		var queryTeamForDeletion = Context.Team.Where(l => l.SectionCode == request.Id).AsNoTracking();
		if (entity.TeamList?.Count > 0)
		{
			queryTeamForDeletion = queryTeamForDeletion.Where(l => !(entity.TeamList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		teamListForDeletion = await queryTeamForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var team in teamListForDeletion!)
		{
			Context.Entry(team).State = EntityState.Deleted;
		}
		if (entity.TeamList?.Count > 0)
		{
			foreach (var team in entity.TeamList.Where(l => !teamListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TeamState>(x => x.Id == team.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(team).State = EntityState.Added;
				}
				else
				{
					Context.Entry(team).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditSectionCommandValidator : AbstractValidator<EditSectionCommand>
{
    readonly ApplicationContext _context;

    public EditSectionCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SectionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Section with id {PropertyValue} does not exists");
        RuleFor(x => x.SectionCode).MustAsync(async (request, sectionCode, cancellation) => await _context.NotExists<SectionState>(x => x.SectionCode == sectionCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Section with sectionCode {PropertyValue} already exists");
	
    }
}
