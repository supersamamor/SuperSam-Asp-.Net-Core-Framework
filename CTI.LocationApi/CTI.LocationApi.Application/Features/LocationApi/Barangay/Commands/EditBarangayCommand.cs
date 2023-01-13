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

public record EditBarangayCommand : BarangayState, IRequest<Validation<Error, BarangayState>>;

public class EditBarangayCommandHandler : BaseCommandHandler<ApplicationContext, BarangayState, EditBarangayCommand>, IRequestHandler<EditBarangayCommand, Validation<Error, BarangayState>>
{
    public EditBarangayCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBarangayCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, BarangayState>> Handle(EditBarangayCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditBarangayCommandValidator : AbstractValidator<EditBarangayCommand>
{
    readonly ApplicationContext _context;

    public EditBarangayCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BarangayState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Barangay with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<BarangayState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Barangay with code {PropertyValue} already exists");
	
    }
}
