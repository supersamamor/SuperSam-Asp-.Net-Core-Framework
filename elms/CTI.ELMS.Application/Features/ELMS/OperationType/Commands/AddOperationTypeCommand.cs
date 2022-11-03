using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.OperationType.Commands;

public record AddOperationTypeCommand : OperationTypeState, IRequest<Validation<Error, OperationTypeState>>;

public class AddOperationTypeCommandHandler : BaseCommandHandler<ApplicationContext, OperationTypeState, AddOperationTypeCommand>, IRequestHandler<AddOperationTypeCommand, Validation<Error, OperationTypeState>>
{
	private readonly IdentityContext _identityContext;
    public AddOperationTypeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddOperationTypeCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, OperationTypeState>> Handle(AddOperationTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddOperationTypeCommandValidator : AbstractValidator<AddOperationTypeCommand>
{
    readonly ApplicationContext _context;

    public AddOperationTypeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<OperationTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OperationType with id {PropertyValue} already exists");
        RuleFor(x => x.OperationTypeName).MustAsync(async (operationTypeName, cancellation) => await _context.NotExists<OperationTypeState>(x => x.OperationTypeName == operationTypeName, cancellationToken: cancellation)).WithMessage("OperationType with operationTypeName {PropertyValue} already exists");
	
    }
}
