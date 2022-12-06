using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;

public record DeactivatePayeeCommand : EnrolledPayeeState, IRequest<Validation<Error, EnrolledPayeeState>>;

public class DeactivatePayeeCommandHandler : BaseCommandHandler<ApplicationContext, EnrolledPayeeState, DeactivatePayeeCommand>, IRequestHandler<DeactivatePayeeCommand, Validation<Error, EnrolledPayeeState>>
{
	public DeactivatePayeeCommandHandler(ApplicationContext context,
									 IMapper mapper,
									 CompositeValidator<DeactivatePayeeCommand> validator) : base(context, mapper, validator)
	{
	}

	public async Task<Validation<Error, EnrolledPayeeState>> Handle(DeactivatePayeeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditEnrolledPayee(request, cancellationToken));


	public async Task<Validation<Error, EnrolledPayeeState>> EditEnrolledPayee(DeactivatePayeeCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.EnrolledPayee.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		entity.TagAsInActive();
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, EnrolledPayeeState>(entity);
	}
}

public class DeactivatePayeeCommandValidator : AbstractValidator<DeactivatePayeeCommand>
{
	readonly ApplicationContext _context;

	public DeactivatePayeeCommandValidator(ApplicationContext context)
	{
		_context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EnrolledPayeeState>(x => x.Id == id, cancellationToken: cancellation))
						  .WithMessage("EnrolledPayee with id {PropertyValue} does not exists");

	}
}
