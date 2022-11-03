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

public record EditAnnualIncrementHistoryCommand : AnnualIncrementHistoryState, IRequest<Validation<Error, AnnualIncrementHistoryState>>;

public class EditAnnualIncrementHistoryCommandHandler : BaseCommandHandler<ApplicationContext, AnnualIncrementHistoryState, EditAnnualIncrementHistoryCommand>, IRequestHandler<EditAnnualIncrementHistoryCommand, Validation<Error, AnnualIncrementHistoryState>>
{
    public EditAnnualIncrementHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditAnnualIncrementHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, AnnualIncrementHistoryState>> Handle(EditAnnualIncrementHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditAnnualIncrementHistoryCommandValidator : AbstractValidator<EditAnnualIncrementHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditAnnualIncrementHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<AnnualIncrementHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("AnnualIncrementHistory with id {PropertyValue} does not exists");
        
    }
}
