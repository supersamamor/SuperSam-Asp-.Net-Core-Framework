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

namespace CTI.ELMS.Application.Features.ELMS.BusinessNature.Commands;

public record DeleteBusinessNatureCommand : BaseCommand, IRequest<Validation<Error, BusinessNatureState>>;

public class DeleteBusinessNatureCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureState, DeleteBusinessNatureCommand>, IRequestHandler<DeleteBusinessNatureCommand, Validation<Error, BusinessNatureState>>
{
    public DeleteBusinessNatureCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBusinessNatureCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BusinessNatureState>> Handle(DeleteBusinessNatureCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBusinessNatureCommandValidator : AbstractValidator<DeleteBusinessNatureCommand>
{
    readonly ApplicationContext _context;

    public DeleteBusinessNatureCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessNatureState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNature with id {PropertyValue} does not exists");
    }
}
