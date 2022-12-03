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

public record AddBatchCommand : BatchState, IRequest<Validation<Error, BatchState>>;

public class AddBatchCommandHandler : BaseCommandHandler<ApplicationContext, BatchState, AddBatchCommand>, IRequestHandler<AddBatchCommand, Validation<Error, BatchState>>
{
	private readonly IdentityContext _identityContext;
    public AddBatchCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBatchCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, BatchState>> Handle(AddBatchCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddBatchCommandValidator : AbstractValidator<AddBatchCommand>
{
    readonly ApplicationContext _context;

    public AddBatchCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BatchState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Batch with id {PropertyValue} already exists");
        
    }
}
