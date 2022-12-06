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

public record ReEnrollPayeeCommand : EnrolledPayeeState, IRequest<Validation<Error, EnrolledPayeeState>>;

public class ReEnrollPayeeCommandHandler : BaseCommandHandler<ApplicationContext, EnrolledPayeeState, ReEnrollPayeeCommand>, IRequestHandler<ReEnrollPayeeCommand, Validation<Error, EnrolledPayeeState>>
{
    public ReEnrollPayeeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<ReEnrollPayeeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, EnrolledPayeeState>> Handle(ReEnrollPayeeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditEnrolledPayee(request, cancellationToken));


	public async Task<Validation<Error, EnrolledPayeeState>> EditEnrolledPayee(ReEnrollPayeeCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.EnrolledPayee.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateEnrolledPayeeEmailList(entity, request, cancellationToken);
		entity.TagAsForReEnroll();
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, EnrolledPayeeState>(entity);
	}
	
	private async Task UpdateEnrolledPayeeEmailList(EnrolledPayeeState entity, ReEnrollPayeeCommand request, CancellationToken cancellationToken)
	{
		IList<EnrolledPayeeEmailState> enrolledPayeeEmailListForDeletion = new List<EnrolledPayeeEmailState>();
		var queryEnrolledPayeeEmailForDeletion = Context.EnrolledPayeeEmail.Where(l => l.EnrolledPayeeId == request.Id).AsNoTracking();
		if (entity.EnrolledPayeeEmailList?.Count > 0)
		{
			queryEnrolledPayeeEmailForDeletion = queryEnrolledPayeeEmailForDeletion.Where(l => !(entity.EnrolledPayeeEmailList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		enrolledPayeeEmailListForDeletion = await queryEnrolledPayeeEmailForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var enrolledPayeeEmail in enrolledPayeeEmailListForDeletion!)
		{
			Context.Entry(enrolledPayeeEmail).State = EntityState.Deleted;
		}
		if (entity.EnrolledPayeeEmailList?.Count > 0)
		{
			foreach (var enrolledPayeeEmail in entity.EnrolledPayeeEmailList.Where(l => !enrolledPayeeEmailListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<EnrolledPayeeEmailState>(x => x.Id == enrolledPayeeEmail.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(enrolledPayeeEmail).State = EntityState.Added;
				}
				else
				{
					Context.Entry(enrolledPayeeEmail).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class ReEnrollPayeeCommandValidator : AbstractValidator<ReEnrollPayeeCommand>
{
    readonly ApplicationContext _context;

    public ReEnrollPayeeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EnrolledPayeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EnrolledPayee with id {PropertyValue} does not exists");
        
    }
}
