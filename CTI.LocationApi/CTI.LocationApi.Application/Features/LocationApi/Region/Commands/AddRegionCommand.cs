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

namespace CTI.LocationApi.Application.Features.LocationApi.Region.Commands;

public record AddRegionCommand : RegionState, IRequest<Validation<Error, RegionState>>;

public class AddRegionCommandHandler : BaseCommandHandler<ApplicationContext, RegionState, AddRegionCommand>, IRequestHandler<AddRegionCommand, Validation<Error, RegionState>>
{

    public AddRegionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddRegionCommand> validator
									) : base(context, mapper, validator)
    {
		
    }

    
public async Task<Validation<Error, RegionState>> Handle(AddRegionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddRegionCommandValidator : AbstractValidator<AddRegionCommand>
{
    readonly ApplicationContext _context;

    public AddRegionCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<RegionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Region with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<RegionState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Region with code {PropertyValue} already exists");
	
    }
}
