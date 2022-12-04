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

namespace CTI.FAS.Application.Features.FAS.Creditor.Commands;

public record EditCreditorCommand : CreditorState, IRequest<Validation<Error, CreditorState>>;

public class EditCreditorCommandHandler : BaseCommandHandler<ApplicationContext, CreditorState, EditCreditorCommand>, IRequestHandler<EditCreditorCommand, Validation<Error, CreditorState>>
{
    public EditCreditorCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCreditorCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CreditorState>> Handle(EditCreditorCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCreditorCommandValidator : AbstractValidator<EditCreditorCommand>
{
    readonly ApplicationContext _context;

    public EditCreditorCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CreditorState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Creditor with id {PropertyValue} does not exists");
        
    }
}
