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

public record EditItemCommand : ItemState, IRequest<Validation<Error, ItemState>>;

public class EditItemCommandHandler : BaseCommandHandler<ApplicationContext, ItemState, EditItemCommand>, IRequestHandler<EditItemCommand, Validation<Error, ItemState>>
{
    public EditItemCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditItemCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ItemState>> Handle(EditItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditItemCommandValidator : AbstractValidator<EditItemCommand>
{
    readonly ApplicationContext _context;

    public EditItemCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Item with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<ItemState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Item with code {PropertyValue} already exists");
	RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<ItemState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Item with name {PropertyValue} already exists");
	
    }
}
