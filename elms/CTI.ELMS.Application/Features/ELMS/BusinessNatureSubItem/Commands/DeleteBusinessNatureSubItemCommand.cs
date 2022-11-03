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

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Commands;

public record DeleteBusinessNatureSubItemCommand : BaseCommand, IRequest<Validation<Error, BusinessNatureSubItemState>>;

public class DeleteBusinessNatureSubItemCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureSubItemState, DeleteBusinessNatureSubItemCommand>, IRequestHandler<DeleteBusinessNatureSubItemCommand, Validation<Error, BusinessNatureSubItemState>>
{
    public DeleteBusinessNatureSubItemCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBusinessNatureSubItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BusinessNatureSubItemState>> Handle(DeleteBusinessNatureSubItemCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBusinessNatureSubItemCommandValidator : AbstractValidator<DeleteBusinessNatureSubItemCommand>
{
    readonly ApplicationContext _context;

    public DeleteBusinessNatureSubItemCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessNatureSubItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNatureSubItem with id {PropertyValue} does not exists");
    }
}
