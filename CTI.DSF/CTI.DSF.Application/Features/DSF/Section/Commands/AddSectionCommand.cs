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

public record AddSectionCommand : SectionState, IRequest<Validation<Error, SectionState>>;

public class AddSectionCommandHandler : BaseCommandHandler<ApplicationContext, SectionState, AddSectionCommand>, IRequestHandler<AddSectionCommand, Validation<Error, SectionState>>
{
	private readonly IdentityContext _identityContext;
    public AddSectionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSectionCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, SectionState>> Handle(AddSectionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddSection(request, cancellationToken));


	public async Task<Validation<Error, SectionState>> AddSection(AddSectionCommand request, CancellationToken cancellationToken)
	{
		SectionState entity = Mapper.Map<SectionState>(request);
		UpdateTeamList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SectionState>(entity);
	}
	
	private void UpdateTeamList(SectionState entity)
	{
		if (entity.TeamList?.Count > 0)
		{
			foreach (var team in entity.TeamList!)
			{
				Context.Entry(team).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddSectionCommandValidator : AbstractValidator<AddSectionCommand>
{
    readonly ApplicationContext _context;

    public AddSectionCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SectionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Section with id {PropertyValue} already exists");
        RuleFor(x => x.SectionCode).MustAsync(async (sectionCode, cancellation) => await _context.NotExists<SectionState>(x => x.SectionCode == sectionCode, cancellationToken: cancellation)).WithMessage("Section with sectionCode {PropertyValue} already exists");
	
    }
}
