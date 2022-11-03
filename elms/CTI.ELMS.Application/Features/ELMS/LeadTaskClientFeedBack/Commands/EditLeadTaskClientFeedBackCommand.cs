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

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Commands;

public record EditLeadTaskClientFeedBackCommand : LeadTaskClientFeedBackState, IRequest<Validation<Error, LeadTaskClientFeedBackState>>;

public class EditLeadTaskClientFeedBackCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskClientFeedBackState, EditLeadTaskClientFeedBackCommand>, IRequestHandler<EditLeadTaskClientFeedBackCommand, Validation<Error, LeadTaskClientFeedBackState>>
{
    public EditLeadTaskClientFeedBackCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLeadTaskClientFeedBackCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, LeadTaskClientFeedBackState>> Handle(EditLeadTaskClientFeedBackCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditLeadTaskClientFeedBackCommandValidator : AbstractValidator<EditLeadTaskClientFeedBackCommand>
{
    readonly ApplicationContext _context;

    public EditLeadTaskClientFeedBackCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTaskClientFeedBackState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTaskClientFeedBack with id {PropertyValue} does not exists");
        
    }
}
