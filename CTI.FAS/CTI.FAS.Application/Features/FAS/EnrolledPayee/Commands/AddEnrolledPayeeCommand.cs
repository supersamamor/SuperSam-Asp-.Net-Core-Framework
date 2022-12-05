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

public record AddEnrolledPayeeCommand : EnrolledPayeeState, IRequest<Validation<Error, EnrolledPayeeState>>;

public class AddEnrolledPayeeCommandHandler : BaseCommandHandler<ApplicationContext, EnrolledPayeeState, AddEnrolledPayeeCommand>, IRequestHandler<AddEnrolledPayeeCommand, Validation<Error, EnrolledPayeeState>>
{

    public AddEnrolledPayeeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddEnrolledPayeeCommand> validator) : base(context, mapper, validator)
    {

    }

    public async Task<Validation<Error, EnrolledPayeeState>> Handle(AddEnrolledPayeeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddEnrolledPayee(request, cancellationToken));


	public async Task<Validation<Error, EnrolledPayeeState>> AddEnrolledPayee(AddEnrolledPayeeCommand request, CancellationToken cancellationToken)
	{
		EnrolledPayeeState entity = Mapper.Map<EnrolledPayeeState>(request);
		entity.TagAsActive();
		UpdateEnrolledPayeeEmailList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, EnrolledPayeeState>(entity);
	}
	
	private void UpdateEnrolledPayeeEmailList(EnrolledPayeeState entity)
	{
		if (entity.EnrolledPayeeEmailList?.Count > 0)
		{
			foreach (var enrolledPayeeEmail in entity.EnrolledPayeeEmailList!)
			{
				Context.Entry(enrolledPayeeEmail).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddEnrolledPayeeCommandValidator : AbstractValidator<AddEnrolledPayeeCommand>
{
    readonly ApplicationContext _context;

    public AddEnrolledPayeeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<EnrolledPayeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EnrolledPayee with id {PropertyValue} already exists");
        
    }
}
