using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Assignment.Commands;

public record AddAssignmentCommand : AssignmentState, IRequest<Validation<Error, AssignmentState>>;

public class AddAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, AssignmentState, AddAssignmentCommand>, IRequestHandler<AddAssignmentCommand, Validation<Error, AssignmentState>>
{
	private readonly IdentityContext _identityContext;
    public AddAssignmentCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddAssignmentCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, AssignmentState>> Handle(AddAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddAssignment(request, cancellationToken));


	public async Task<Validation<Error, AssignmentState>> AddAssignment(AddAssignmentCommand request, CancellationToken cancellationToken)
	{
		AssignmentState entity = Mapper.Map<AssignmentState>(request);
		UpdateDeliveryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, AssignmentState>(entity);
	}
	
	private void UpdateDeliveryList(AssignmentState entity)
	{
		if (entity.DeliveryList?.Count > 0)
		{
			foreach (var delivery in entity.DeliveryList!)
			{
				Context.Entry(delivery).State = EntityState.Added;
			}
		}
	}
	
}

public class AddAssignmentCommandValidator : AbstractValidator<AddAssignmentCommand>
{
    readonly ApplicationContext _context;

    public AddAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<AssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Assignment with id {PropertyValue} already exists");
        RuleFor(x => x.AssignmentCode).MustAsync(async (assignmentCode, cancellation) => await _context.NotExists<AssignmentState>(x => x.AssignmentCode == assignmentCode, cancellationToken: cancellation)).WithMessage("Assignment with assignmentCode {PropertyValue} already exists");
	
    }
}
