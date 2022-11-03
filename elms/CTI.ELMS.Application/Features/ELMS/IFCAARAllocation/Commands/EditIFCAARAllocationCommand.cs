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

public record EditIFCAARAllocationCommand : IFCAARAllocationState, IRequest<Validation<Error, IFCAARAllocationState>>;

public class EditIFCAARAllocationCommandHandler : BaseCommandHandler<ApplicationContext, IFCAARAllocationState, EditIFCAARAllocationCommand>, IRequestHandler<EditIFCAARAllocationCommand, Validation<Error, IFCAARAllocationState>>
{
    public EditIFCAARAllocationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditIFCAARAllocationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, IFCAARAllocationState>> Handle(EditIFCAARAllocationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditIFCAARAllocationCommandValidator : AbstractValidator<EditIFCAARAllocationCommand>
{
    readonly ApplicationContext _context;

    public EditIFCAARAllocationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCAARAllocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAARAllocation with id {PropertyValue} does not exists");
        
    }
}
