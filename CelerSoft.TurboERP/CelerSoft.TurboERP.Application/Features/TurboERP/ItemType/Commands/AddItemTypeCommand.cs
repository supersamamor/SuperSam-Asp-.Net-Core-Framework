using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Commands;

public record AddItemTypeCommand : ItemTypeState, IRequest<Validation<Error, ItemTypeState>>;

public class AddItemTypeCommandHandler : BaseCommandHandler<ApplicationContext, ItemTypeState, AddItemTypeCommand>, IRequestHandler<AddItemTypeCommand, Validation<Error, ItemTypeState>>
{
	private readonly IdentityContext _identityContext;
    public AddItemTypeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddItemTypeCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ItemTypeState>> Handle(AddItemTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddItemTypeCommandValidator : AbstractValidator<AddItemTypeCommand>
{
    readonly ApplicationContext _context;

    public AddItemTypeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ItemTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ItemType with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<ItemTypeState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("ItemType with name {PropertyValue} already exists");
	
    }
}
