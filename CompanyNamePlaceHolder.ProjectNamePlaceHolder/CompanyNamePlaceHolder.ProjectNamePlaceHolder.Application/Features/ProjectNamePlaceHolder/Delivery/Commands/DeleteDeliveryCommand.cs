using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Delivery.Commands;

public record DeleteDeliveryCommand : BaseCommand, IRequest<Validation<Error, DeliveryState>>;

public class DeleteDeliveryCommandHandler : BaseCommandHandler<ApplicationContext, DeliveryState, DeleteDeliveryCommand>, IRequestHandler<DeleteDeliveryCommand, Validation<Error, DeliveryState>>
{
    public DeleteDeliveryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteDeliveryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DeliveryState>> Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteDeliveryCommandValidator : AbstractValidator<DeleteDeliveryCommand>
{
    readonly ApplicationContext _context;

    public DeleteDeliveryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DeliveryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Delivery with id {PropertyValue} does not exists");
    }
}
