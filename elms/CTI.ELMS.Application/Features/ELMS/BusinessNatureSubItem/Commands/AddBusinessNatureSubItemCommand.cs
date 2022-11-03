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

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Commands;

public record AddBusinessNatureSubItemCommand : BusinessNatureSubItemState, IRequest<Validation<Error, BusinessNatureSubItemState>>;

public class AddBusinessNatureSubItemCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureSubItemState, AddBusinessNatureSubItemCommand>, IRequestHandler<AddBusinessNatureSubItemCommand, Validation<Error, BusinessNatureSubItemState>>
{
	private readonly IdentityContext _identityContext;
    public AddBusinessNatureSubItemCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBusinessNatureSubItemCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, BusinessNatureSubItemState>> Handle(AddBusinessNatureSubItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddBusinessNatureSubItemCommandValidator : AbstractValidator<AddBusinessNatureSubItemCommand>
{
    readonly ApplicationContext _context;

    public AddBusinessNatureSubItemCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BusinessNatureSubItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNatureSubItem with id {PropertyValue} already exists");
        
    }
}
