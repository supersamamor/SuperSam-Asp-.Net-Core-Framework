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

public record AddWebContentCommand : WebContentState, IRequest<Validation<Error, WebContentState>>;

public class AddWebContentCommandHandler : BaseCommandHandler<ApplicationContext, WebContentState, AddWebContentCommand>, IRequestHandler<AddWebContentCommand, Validation<Error, WebContentState>>
{
	private readonly IdentityContext _identityContext;
    public AddWebContentCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddWebContentCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, WebContentState>> Handle(AddWebContentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddWebContentCommandValidator : AbstractValidator<AddWebContentCommand>
{
    readonly ApplicationContext _context;

    public AddWebContentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<WebContentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("WebContent with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<WebContentState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("WebContent with code {PropertyValue} already exists");
	
    }
}
