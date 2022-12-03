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

namespace CTI.FAS.Application.Features.FAS.CreditorEmail.Commands;

public record EditCreditorEmailCommand : CreditorEmailState, IRequest<Validation<Error, CreditorEmailState>>;

public class EditCreditorEmailCommandHandler : BaseCommandHandler<ApplicationContext, CreditorEmailState, EditCreditorEmailCommand>, IRequestHandler<EditCreditorEmailCommand, Validation<Error, CreditorEmailState>>
{
    public EditCreditorEmailCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCreditorEmailCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CreditorEmailState>> Handle(EditCreditorEmailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCreditorEmailCommandValidator : AbstractValidator<EditCreditorEmailCommand>
{
    readonly ApplicationContext _context;

    public EditCreditorEmailCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CreditorEmailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CreditorEmail with id {PropertyValue} does not exists");
        
    }
}
