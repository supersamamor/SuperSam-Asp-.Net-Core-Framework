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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Commands;

public record DeleteWebContentCommand : BaseCommand, IRequest<Validation<Error, WebContentState>>;

public class DeleteWebContentCommandHandler : BaseCommandHandler<ApplicationContext, WebContentState, DeleteWebContentCommand>, IRequestHandler<DeleteWebContentCommand, Validation<Error, WebContentState>>
{
    public DeleteWebContentCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteWebContentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, WebContentState>> Handle(DeleteWebContentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteWebContentCommandValidator : AbstractValidator<DeleteWebContentCommand>
{
    readonly ApplicationContext _context;

    public DeleteWebContentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<WebContentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("WebContent with id {PropertyValue} does not exists");
    }
}
