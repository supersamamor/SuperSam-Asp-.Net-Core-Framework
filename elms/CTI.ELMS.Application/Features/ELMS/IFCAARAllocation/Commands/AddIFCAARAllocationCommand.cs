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

namespace CTI.ELMS.Application.Features.ELMS.IFCAARAllocation.Commands;

public record AddIFCAARAllocationCommand : IFCAARAllocationState, IRequest<Validation<Error, IFCAARAllocationState>>;

public class AddIFCAARAllocationCommandHandler : BaseCommandHandler<ApplicationContext, IFCAARAllocationState, AddIFCAARAllocationCommand>, IRequestHandler<AddIFCAARAllocationCommand, Validation<Error, IFCAARAllocationState>>
{
	private readonly IdentityContext _identityContext;
    public AddIFCAARAllocationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddIFCAARAllocationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, IFCAARAllocationState>> Handle(AddIFCAARAllocationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddIFCAARAllocationCommandValidator : AbstractValidator<AddIFCAARAllocationCommand>
{
    readonly ApplicationContext _context;

    public AddIFCAARAllocationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<IFCAARAllocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAARAllocation with id {PropertyValue} already exists");
        
    }
}
