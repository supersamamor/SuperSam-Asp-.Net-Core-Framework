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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Item.Commands;

public record AddItemCommand : ItemState, IRequest<Validation<Error, ItemState>>;

public class AddItemCommandHandler : BaseCommandHandler<ApplicationContext, ItemState, AddItemCommand>, IRequestHandler<AddItemCommand, Validation<Error, ItemState>>
{
	private readonly IdentityContext _identityContext;
    public AddItemCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddItemCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ItemState>> Handle(AddItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    readonly ApplicationContext _context;

    public AddItemCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Item with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<ItemState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Item with code {PropertyValue} already exists");
	RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<ItemState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Item with name {PropertyValue} already exists");
	
    }
}
