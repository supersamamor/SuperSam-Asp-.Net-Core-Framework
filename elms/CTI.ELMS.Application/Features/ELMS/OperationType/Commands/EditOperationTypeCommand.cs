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

public record EditOperationTypeCommand : OperationTypeState, IRequest<Validation<Error, OperationTypeState>>;

public class EditOperationTypeCommandHandler : BaseCommandHandler<ApplicationContext, OperationTypeState, EditOperationTypeCommand>, IRequestHandler<EditOperationTypeCommand, Validation<Error, OperationTypeState>>
{
    public EditOperationTypeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditOperationTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, OperationTypeState>> Handle(EditOperationTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditOperationTypeCommandValidator : AbstractValidator<EditOperationTypeCommand>
{
    readonly ApplicationContext _context;

    public EditOperationTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OperationTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OperationType with id {PropertyValue} does not exists");
        RuleFor(x => x.OperationTypeName).MustAsync(async (request, operationTypeName, cancellation) => await _context.NotExists<OperationTypeState>(x => x.OperationTypeName == operationTypeName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("OperationType with operationTypeName {PropertyValue} already exists");
	
    }
}
