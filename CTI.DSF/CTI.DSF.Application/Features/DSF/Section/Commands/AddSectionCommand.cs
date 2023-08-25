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
			async request => await Add(request, cancellationToken));
	
	
}

public class AddSectionCommandValidator : AbstractValidator<AddSectionCommand>
{
    readonly ApplicationContext _context;

    public AddSectionCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SectionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Section with id {PropertyValue} already exists");
        
    }
}
