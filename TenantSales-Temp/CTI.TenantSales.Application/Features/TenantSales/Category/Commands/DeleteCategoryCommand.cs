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

namespace CTI.TenantSales.Application.Features.TenantSales.Category.Commands;

public record DeleteCategoryCommand : BaseCommand, IRequest<Validation<Error, CategoryState>>;

public class DeleteCategoryCommandHandler : BaseCommandHandler<ApplicationContext, CategoryState, DeleteCategoryCommand>, IRequestHandler<DeleteCategoryCommand, Validation<Error, CategoryState>>
{
    public DeleteCategoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CategoryState>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Category with id {PropertyValue} does not exists");
    }
}
