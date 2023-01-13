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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.LocationApi.Application.Features.LocationApi.Barangay.Commands;

public record AddBarangayCommand : BarangayState, IRequest<Validation<Error, BarangayState>>;

public class AddBarangayCommandHandler : BaseCommandHandler<ApplicationContext, BarangayState, AddBarangayCommand>, IRequestHandler<AddBarangayCommand, Validation<Error, BarangayState>>
{

    public AddBarangayCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBarangayCommand> validator) : base(context, mapper, validator)
    {

    }

    
public async Task<Validation<Error, BarangayState>> Handle(AddBarangayCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddBarangayCommandValidator : AbstractValidator<AddBarangayCommand>
{
    readonly ApplicationContext _context;

    public AddBarangayCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BarangayState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Barangay with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<BarangayState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Barangay with code {PropertyValue} already exists");
	
    }
}
