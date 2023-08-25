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
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSectionCommandValidator : AbstractValidator<EditSectionCommand>
{
    readonly ApplicationContext _context;

    public EditSectionCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SectionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Section with id {PropertyValue} does not exists");
        
    }
}
