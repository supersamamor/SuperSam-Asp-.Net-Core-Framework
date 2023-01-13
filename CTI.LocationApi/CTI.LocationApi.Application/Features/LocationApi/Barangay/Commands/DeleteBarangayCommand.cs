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

namespace CTI.LocationApi.Application.Features.LocationApi.Barangay.Commands;

public record DeleteBarangayCommand : BaseCommand, IRequest<Validation<Error, BarangayState>>;

public class DeleteBarangayCommandHandler : BaseCommandHandler<ApplicationContext, BarangayState, DeleteBarangayCommand>, IRequestHandler<DeleteBarangayCommand, Validation<Error, BarangayState>>
{
    public DeleteBarangayCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBarangayCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BarangayState>> Handle(DeleteBarangayCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBarangayCommandValidator : AbstractValidator<DeleteBarangayCommand>
{
    readonly ApplicationContext _context;

    public DeleteBarangayCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BarangayState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Barangay with id {PropertyValue} does not exists");
    }
}
