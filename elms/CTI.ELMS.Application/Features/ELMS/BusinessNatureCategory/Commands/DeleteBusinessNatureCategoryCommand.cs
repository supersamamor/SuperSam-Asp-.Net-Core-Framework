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

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Commands;

public record DeleteBusinessNatureCategoryCommand : BaseCommand, IRequest<Validation<Error, BusinessNatureCategoryState>>;

public class DeleteBusinessNatureCategoryCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureCategoryState, DeleteBusinessNatureCategoryCommand>, IRequestHandler<DeleteBusinessNatureCategoryCommand, Validation<Error, BusinessNatureCategoryState>>
{
    public DeleteBusinessNatureCategoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBusinessNatureCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BusinessNatureCategoryState>> Handle(DeleteBusinessNatureCategoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBusinessNatureCategoryCommandValidator : AbstractValidator<DeleteBusinessNatureCategoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteBusinessNatureCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessNatureCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNatureCategory with id {PropertyValue} does not exists");
    }
}
