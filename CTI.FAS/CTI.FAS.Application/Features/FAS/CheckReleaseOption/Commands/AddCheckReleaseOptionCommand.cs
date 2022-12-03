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

public record AddCheckReleaseOptionCommand : CheckReleaseOptionState, IRequest<Validation<Error, CheckReleaseOptionState>>;

public class AddCheckReleaseOptionCommandHandler : BaseCommandHandler<ApplicationContext, CheckReleaseOptionState, AddCheckReleaseOptionCommand>, IRequestHandler<AddCheckReleaseOptionCommand, Validation<Error, CheckReleaseOptionState>>
{
	private readonly IdentityContext _identityContext;
    public AddCheckReleaseOptionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCheckReleaseOptionCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, CheckReleaseOptionState>> Handle(AddCheckReleaseOptionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddCheckReleaseOptionCommandValidator : AbstractValidator<AddCheckReleaseOptionCommand>
{
    readonly ApplicationContext _context;

    public AddCheckReleaseOptionCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CheckReleaseOptionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CheckReleaseOption with id {PropertyValue} already exists");
        
    }
}
