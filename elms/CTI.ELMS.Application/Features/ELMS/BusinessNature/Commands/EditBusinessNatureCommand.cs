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

namespace CTI.ELMS.Application.Features.ELMS.BusinessNature.Commands;

public record EditBusinessNatureCommand : BusinessNatureState, IRequest<Validation<Error, BusinessNatureState>>;

public class EditBusinessNatureCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureState, EditBusinessNatureCommand>, IRequestHandler<EditBusinessNatureCommand, Validation<Error, BusinessNatureState>>
{
    public EditBusinessNatureCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBusinessNatureCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, BusinessNatureState>> Handle(EditBusinessNatureCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditBusinessNatureCommandValidator : AbstractValidator<EditBusinessNatureCommand>
{
    readonly ApplicationContext _context;

    public EditBusinessNatureCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessNatureState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNature with id {PropertyValue} does not exists");
        RuleFor(x => x.BusinessNatureName).MustAsync(async (request, businessNatureName, cancellation) => await _context.NotExists<BusinessNatureState>(x => x.BusinessNatureName == businessNatureName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("BusinessNature with businessNatureName {PropertyValue} already exists");
	RuleFor(x => x.BusinessNatureCode).MustAsync(async (request, businessNatureCode, cancellation) => await _context.NotExists<BusinessNatureState>(x => x.BusinessNatureCode == businessNatureCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("BusinessNature with businessNatureCode {PropertyValue} already exists");
	
    }
}
