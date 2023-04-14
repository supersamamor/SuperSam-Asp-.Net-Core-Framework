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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Commands;

public record EditSupplierContactPersonCommand : SupplierContactPersonState, IRequest<Validation<Error, SupplierContactPersonState>>;

public class EditSupplierContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, SupplierContactPersonState, EditSupplierContactPersonCommand>, IRequestHandler<EditSupplierContactPersonCommand, Validation<Error, SupplierContactPersonState>>
{
    public EditSupplierContactPersonCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSupplierContactPersonCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SupplierContactPersonState>> Handle(EditSupplierContactPersonCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSupplierContactPersonCommandValidator : AbstractValidator<EditSupplierContactPersonCommand>
{
    readonly ApplicationContext _context;

    public EditSupplierContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierContactPerson with id {PropertyValue} does not exists");
        
    }
}
