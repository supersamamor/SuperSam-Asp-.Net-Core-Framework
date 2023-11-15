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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Holiday.Commands;

public record EditHolidayCommand : HolidayState, IRequest<Validation<Error, HolidayState>>;

public class EditHolidayCommandHandler : BaseCommandHandler<ApplicationContext, HolidayState, EditHolidayCommand>, IRequestHandler<EditHolidayCommand, Validation<Error, HolidayState>>
{
    public EditHolidayCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditHolidayCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, HolidayState>> Handle(EditHolidayCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditHolidayCommandValidator : AbstractValidator<EditHolidayCommand>
{
    readonly ApplicationContext _context;

    public EditHolidayCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<HolidayState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Holiday with id {PropertyValue} does not exists");
        RuleFor(x => x.HolidayName).MustAsync(async (request, holidayName, cancellation) => await _context.NotExists<HolidayState>(x => x.HolidayName == holidayName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Holiday with holidayName {PropertyValue} already exists");
	
    }
}
