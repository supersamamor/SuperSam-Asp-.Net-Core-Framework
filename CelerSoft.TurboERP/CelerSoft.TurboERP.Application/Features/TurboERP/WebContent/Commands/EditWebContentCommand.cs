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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Commands;

public record EditWebContentCommand : WebContentState, IRequest<Validation<Error, WebContentState>>;

public class EditWebContentCommandHandler : BaseCommandHandler<ApplicationContext, WebContentState, EditWebContentCommand>, IRequestHandler<EditWebContentCommand, Validation<Error, WebContentState>>
{
    public EditWebContentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditWebContentCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, WebContentState>> Handle(EditWebContentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditWebContentCommandValidator : AbstractValidator<EditWebContentCommand>
{
    readonly ApplicationContext _context;

    public EditWebContentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<WebContentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("WebContent with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<WebContentState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("WebContent with code {PropertyValue} already exists");
	
    }
}
