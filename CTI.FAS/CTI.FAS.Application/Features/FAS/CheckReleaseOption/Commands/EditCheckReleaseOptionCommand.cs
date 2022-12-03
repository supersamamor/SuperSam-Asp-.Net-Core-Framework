using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.CheckReleaseOption.Commands;

public record EditCheckReleaseOptionCommand : CheckReleaseOptionState, IRequest<Validation<Error, CheckReleaseOptionState>>;

public class EditCheckReleaseOptionCommandHandler : BaseCommandHandler<ApplicationContext, CheckReleaseOptionState, EditCheckReleaseOptionCommand>, IRequestHandler<EditCheckReleaseOptionCommand, Validation<Error, CheckReleaseOptionState>>
{
    public EditCheckReleaseOptionCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCheckReleaseOptionCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CheckReleaseOptionState>> Handle(EditCheckReleaseOptionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCheckReleaseOptionCommandValidator : AbstractValidator<EditCheckReleaseOptionCommand>
{
    readonly ApplicationContext _context;

    public EditCheckReleaseOptionCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CheckReleaseOptionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CheckReleaseOption with id {PropertyValue} does not exists");
        
    }
}
