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

public record EditBusinessNatureSubItemCommand : BusinessNatureSubItemState, IRequest<Validation<Error, BusinessNatureSubItemState>>;

public class EditBusinessNatureSubItemCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureSubItemState, EditBusinessNatureSubItemCommand>, IRequestHandler<EditBusinessNatureSubItemCommand, Validation<Error, BusinessNatureSubItemState>>
{
    public EditBusinessNatureSubItemCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBusinessNatureSubItemCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, BusinessNatureSubItemState>> Handle(EditBusinessNatureSubItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditBusinessNatureSubItemCommandValidator : AbstractValidator<EditBusinessNatureSubItemCommand>
{
    readonly ApplicationContext _context;

    public EditBusinessNatureSubItemCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessNatureSubItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNatureSubItem with id {PropertyValue} does not exists");
        
    }
}
