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

namespace CTI.FAS.Application.Features.FAS.Batch.Commands;

public record EditBatchCommand : BatchState, IRequest<Validation<Error, BatchState>>;

public class EditBatchCommandHandler : BaseCommandHandler<ApplicationContext, BatchState, EditBatchCommand>, IRequestHandler<EditBatchCommand, Validation<Error, BatchState>>
{
    public EditBatchCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBatchCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, BatchState>> Handle(EditBatchCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditBatchCommandValidator : AbstractValidator<EditBatchCommand>
{
    readonly ApplicationContext _context;

    public EditBatchCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BatchState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Batch with id {PropertyValue} does not exists");
        
    }
}
