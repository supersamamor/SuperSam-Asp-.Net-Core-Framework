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

public record AddProvinceCommand : ProvinceState, IRequest<Validation<Error, ProvinceState>>;

public class AddProvinceCommandHandler : BaseCommandHandler<ApplicationContext, ProvinceState, AddProvinceCommand>, IRequestHandler<AddProvinceCommand, Validation<Error, ProvinceState>>
{

    public AddProvinceCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProvinceCommand> validator) : base(context, mapper, validator)
    {
		
    }

    
public async Task<Validation<Error, ProvinceState>> Handle(AddProvinceCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProvinceCommandValidator : AbstractValidator<AddProvinceCommand>
{
    readonly ApplicationContext _context;

    public AddProvinceCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProvinceState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Province with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<ProvinceState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Province with code {PropertyValue} already exists");
	
    }
}
