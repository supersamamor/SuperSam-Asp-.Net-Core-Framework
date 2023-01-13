using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.LocationApi.Application.Features.LocationApi.Province.Commands;

public record DeleteProvinceCommand : BaseCommand, IRequest<Validation<Error, ProvinceState>>;

public class DeleteProvinceCommandHandler : BaseCommandHandler<ApplicationContext, ProvinceState, DeleteProvinceCommand>, IRequestHandler<DeleteProvinceCommand, Validation<Error, ProvinceState>>
{
    public DeleteProvinceCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProvinceCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProvinceState>> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProvinceCommandValidator : AbstractValidator<DeleteProvinceCommand>
{
    readonly ApplicationContext _context;

    public DeleteProvinceCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProvinceState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Province with id {PropertyValue} does not exists");
    }
}
