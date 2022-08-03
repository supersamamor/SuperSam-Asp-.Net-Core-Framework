using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.Revalidate.Commands;

public record EditRevalidateCommand : RevalidateState, IRequest<Validation<Error, RevalidateState>>;

public class EditRevalidateCommandHandler : BaseCommandHandler<ApplicationContext, RevalidateState, EditRevalidateCommand>, IRequestHandler<EditRevalidateCommand, Validation<Error, RevalidateState>>
{
    public EditRevalidateCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditRevalidateCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, RevalidateState>> Handle(EditRevalidateCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditRevalidateCommandValidator : AbstractValidator<EditRevalidateCommand>
{
    readonly ApplicationContext _context;

    public EditRevalidateCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RevalidateState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Revalidate with id {PropertyValue} does not exists");
        
    }
}
