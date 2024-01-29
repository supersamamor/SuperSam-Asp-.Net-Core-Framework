using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Commands;

public record EditContactInformationCommand : ContactInformationState, IRequest<Validation<Error, ContactInformationState>>;

public class EditContactInformationCommandHandler : BaseCommandHandler<ApplicationContext, ContactInformationState, EditContactInformationCommand>, IRequestHandler<EditContactInformationCommand, Validation<Error, ContactInformationState>>
{
    public EditContactInformationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditContactInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ContactInformationState>> Handle(EditContactInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditContactInformationCommandValidator : AbstractValidator<EditContactInformationCommand>
{
    readonly ApplicationContext _context;

    public EditContactInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ContactInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ContactInformation with id {PropertyValue} does not exists");
        
    }
}
