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

namespace CTI.LocationApi.Application.Features.LocationApi.Province.Commands;

public record EditProvinceCommand : ProvinceState, IRequest<Validation<Error, ProvinceState>>;

public class EditProvinceCommandHandler : BaseCommandHandler<ApplicationContext, ProvinceState, EditProvinceCommand>, IRequestHandler<EditProvinceCommand, Validation<Error, ProvinceState>>
{
    public EditProvinceCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProvinceCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProvinceState>> Handle(EditProvinceCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProvinceCommandValidator : AbstractValidator<EditProvinceCommand>
{
    readonly ApplicationContext _context;

    public EditProvinceCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProvinceState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Province with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<ProvinceState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Province with code {PropertyValue} already exists");
	
    }
}
