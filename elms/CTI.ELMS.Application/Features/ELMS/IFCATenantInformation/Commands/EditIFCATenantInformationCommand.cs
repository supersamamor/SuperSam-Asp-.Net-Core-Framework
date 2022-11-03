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

namespace CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Commands;

public record EditIFCATenantInformationCommand : IFCATenantInformationState, IRequest<Validation<Error, IFCATenantInformationState>>;

public class EditIFCATenantInformationCommandHandler : BaseCommandHandler<ApplicationContext, IFCATenantInformationState, EditIFCATenantInformationCommand>, IRequestHandler<EditIFCATenantInformationCommand, Validation<Error, IFCATenantInformationState>>
{
    public EditIFCATenantInformationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditIFCATenantInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, IFCATenantInformationState>> Handle(EditIFCATenantInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditIFCATenantInformationCommandValidator : AbstractValidator<EditIFCATenantInformationCommand>
{
    readonly ApplicationContext _context;

    public EditIFCATenantInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCATenantInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCATenantInformation with id {PropertyValue} does not exists");
        
    }
}
