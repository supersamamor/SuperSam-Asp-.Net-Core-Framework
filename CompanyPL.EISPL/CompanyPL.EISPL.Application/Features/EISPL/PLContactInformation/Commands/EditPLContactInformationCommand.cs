using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Commands;

public record EditPLContactInformationCommand : PLContactInformationState, IRequest<Validation<Error, PLContactInformationState>>;

public class EditPLContactInformationCommandHandler : BaseCommandHandler<ApplicationContext, PLContactInformationState, EditPLContactInformationCommand>, IRequestHandler<EditPLContactInformationCommand, Validation<Error, PLContactInformationState>>
{
    public EditPLContactInformationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPLContactInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PLContactInformationState>> Handle(EditPLContactInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPLContactInformationCommandValidator : AbstractValidator<EditPLContactInformationCommand>
{
    readonly ApplicationContext _context;

    public EditPLContactInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PLContactInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLContactInformation with id {PropertyValue} does not exists");
        
    }
}
