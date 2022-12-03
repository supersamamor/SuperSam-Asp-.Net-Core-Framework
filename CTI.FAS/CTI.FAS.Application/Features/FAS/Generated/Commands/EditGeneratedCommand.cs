using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.Generated.Commands;

public record EditGeneratedCommand : GeneratedState, IRequest<Validation<Error, GeneratedState>>;

public class EditGeneratedCommandHandler : BaseCommandHandler<ApplicationContext, GeneratedState, EditGeneratedCommand>, IRequestHandler<EditGeneratedCommand, Validation<Error, GeneratedState>>
{
    public EditGeneratedCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditGeneratedCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, GeneratedState>> Handle(EditGeneratedCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditGeneratedCommandValidator : AbstractValidator<EditGeneratedCommand>
{
    readonly ApplicationContext _context;

    public EditGeneratedCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<GeneratedState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Generated with id {PropertyValue} does not exists");
        
    }
}
