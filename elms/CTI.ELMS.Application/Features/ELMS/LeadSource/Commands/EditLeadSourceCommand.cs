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

namespace CTI.ELMS.Application.Features.ELMS.LeadSource.Commands;

public record EditLeadSourceCommand : LeadSourceState, IRequest<Validation<Error, LeadSourceState>>;

public class EditLeadSourceCommandHandler : BaseCommandHandler<ApplicationContext, LeadSourceState, EditLeadSourceCommand>, IRequestHandler<EditLeadSourceCommand, Validation<Error, LeadSourceState>>
{
    public EditLeadSourceCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLeadSourceCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, LeadSourceState>> Handle(EditLeadSourceCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditLeadSourceCommandValidator : AbstractValidator<EditLeadSourceCommand>
{
    readonly ApplicationContext _context;

    public EditLeadSourceCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadSourceState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadSource with id {PropertyValue} does not exists");
        RuleFor(x => x.LeadSourceName).MustAsync(async (request, leadSourceName, cancellation) => await _context.NotExists<LeadSourceState>(x => x.LeadSourceName == leadSourceName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("LeadSource with leadSourceName {PropertyValue} already exists");
	
    }
}
