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

public record AddHolidayCommand : HolidayState, IRequest<Validation<Error, HolidayState>>;

public class AddHolidayCommandHandler : BaseCommandHandler<ApplicationContext, HolidayState, AddHolidayCommand>, IRequestHandler<AddHolidayCommand, Validation<Error, HolidayState>>
{
	private readonly IdentityContext _identityContext;
    public AddHolidayCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddHolidayCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, HolidayState>> Handle(AddHolidayCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddHolidayCommandValidator : AbstractValidator<AddHolidayCommand>
{
    readonly ApplicationContext _context;

    public AddHolidayCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<HolidayState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Holiday with id {PropertyValue} already exists");
        RuleFor(x => x.HolidayName).MustAsync(async (holidayName, cancellation) => await _context.NotExists<HolidayState>(x => x.HolidayName == holidayName, cancellationToken: cancellation)).WithMessage("Holiday with holidayName {PropertyValue} already exists");
	
    }
}
