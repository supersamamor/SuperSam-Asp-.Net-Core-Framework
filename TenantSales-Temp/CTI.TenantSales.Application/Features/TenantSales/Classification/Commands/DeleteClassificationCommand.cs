using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.TenantSales.Application.Features.TenantSales.Classification.Commands;

public record DeleteClassificationCommand : BaseCommand, IRequest<Validation<Error, ClassificationState>>;

public class DeleteClassificationCommandHandler : BaseCommandHandler<ApplicationContext, ClassificationState, DeleteClassificationCommand>, IRequestHandler<DeleteClassificationCommand, Validation<Error, ClassificationState>>
{
    public DeleteClassificationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteClassificationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ClassificationState>> Handle(DeleteClassificationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteClassificationCommandValidator : AbstractValidator<DeleteClassificationCommand>
{
    readonly ApplicationContext _context;

    public DeleteClassificationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ClassificationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Classification with id {PropertyValue} does not exists");
    }
}
