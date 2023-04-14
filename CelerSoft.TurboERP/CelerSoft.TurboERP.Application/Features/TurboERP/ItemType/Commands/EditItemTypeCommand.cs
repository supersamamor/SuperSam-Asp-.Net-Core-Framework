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

public record EditItemTypeCommand : ItemTypeState, IRequest<Validation<Error, ItemTypeState>>;

public class EditItemTypeCommandHandler : BaseCommandHandler<ApplicationContext, ItemTypeState, EditItemTypeCommand>, IRequestHandler<EditItemTypeCommand, Validation<Error, ItemTypeState>>
{
    public EditItemTypeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditItemTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ItemTypeState>> Handle(EditItemTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditItemTypeCommandValidator : AbstractValidator<EditItemTypeCommand>
{
    readonly ApplicationContext _context;

    public EditItemTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ItemTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ItemType with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<ItemTypeState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ItemType with name {PropertyValue} already exists");
	
    }
}
