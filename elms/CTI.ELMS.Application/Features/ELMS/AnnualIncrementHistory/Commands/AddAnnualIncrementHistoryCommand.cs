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

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrementHistory.Commands;

public record AddAnnualIncrementHistoryCommand : AnnualIncrementHistoryState, IRequest<Validation<Error, AnnualIncrementHistoryState>>;

public class AddAnnualIncrementHistoryCommandHandler : BaseCommandHandler<ApplicationContext, AnnualIncrementHistoryState, AddAnnualIncrementHistoryCommand>, IRequestHandler<AddAnnualIncrementHistoryCommand, Validation<Error, AnnualIncrementHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddAnnualIncrementHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddAnnualIncrementHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, AnnualIncrementHistoryState>> Handle(AddAnnualIncrementHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddAnnualIncrementHistoryCommandValidator : AbstractValidator<AddAnnualIncrementHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddAnnualIncrementHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<AnnualIncrementHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("AnnualIncrementHistory with id {PropertyValue} already exists");
        
    }
}
