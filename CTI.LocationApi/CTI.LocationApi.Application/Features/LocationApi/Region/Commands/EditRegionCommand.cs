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

public record EditRegionCommand : RegionState, IRequest<Validation<Error, RegionState>>;

public class EditRegionCommandHandler : BaseCommandHandler<ApplicationContext, RegionState, EditRegionCommand>, IRequestHandler<EditRegionCommand, Validation<Error, RegionState>>
{
    public EditRegionCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditRegionCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, RegionState>> Handle(EditRegionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditRegionCommandValidator : AbstractValidator<EditRegionCommand>
{
    readonly ApplicationContext _context;

    public EditRegionCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RegionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Region with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<RegionState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Region with code {PropertyValue} already exists");
	
    }
}
