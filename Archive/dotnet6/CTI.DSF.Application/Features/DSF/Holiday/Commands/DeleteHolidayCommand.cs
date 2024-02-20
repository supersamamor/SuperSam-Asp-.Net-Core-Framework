using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.DSF.Application.Features.DSF.Holiday.Commands;

public record DeleteHolidayCommand : BaseCommand, IRequest<Validation<Error, HolidayState>>;

public class DeleteHolidayCommandHandler : BaseCommandHandler<ApplicationContext, HolidayState, DeleteHolidayCommand>, IRequestHandler<DeleteHolidayCommand, Validation<Error, HolidayState>>
{
    public DeleteHolidayCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteHolidayCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, HolidayState>> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteHolidayCommandValidator : AbstractValidator<DeleteHolidayCommand>
{
    readonly ApplicationContext _context;

    public DeleteHolidayCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<HolidayState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Holiday with id {PropertyValue} does not exists");
    }
}
