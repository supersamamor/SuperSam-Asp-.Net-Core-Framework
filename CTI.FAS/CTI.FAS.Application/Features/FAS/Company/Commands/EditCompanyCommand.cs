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

namespace CTI.FAS.Application.Features.FAS.Company.Commands;

public record EditCompanyCommand : CompanyState, IRequest<Validation<Error, CompanyState>>;

public class EditCompanyCommandHandler : BaseCommandHandler<ApplicationContext, CompanyState, EditCompanyCommand>, IRequestHandler<EditCompanyCommand, Validation<Error, CompanyState>>
{
    public EditCompanyCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCompanyCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CompanyState>> Handle(EditCompanyCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditCompany(request, cancellationToken));


	public async Task<Validation<Error, CompanyState>> EditCompany(EditCompanyCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Company.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateUserEntityList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CompanyState>(entity);
	}
	
	private async Task UpdateUserEntityList(CompanyState entity, EditCompanyCommand request, CancellationToken cancellationToken)
	{
		IList<UserEntityState> userEntityListForDeletion = new List<UserEntityState>();
		var queryUserEntityForDeletion = Context.UserEntity.Where(l => l.CompanyId == request.Id).AsNoTracking();
		if (entity.UserEntityList?.Count > 0)
		{
			queryUserEntityForDeletion = queryUserEntityForDeletion.Where(l => !(entity.UserEntityList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		userEntityListForDeletion = await queryUserEntityForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var userEntity in userEntityListForDeletion!)
		{
			Context.Entry(userEntity).State = EntityState.Deleted;
		}
		if (entity.UserEntityList?.Count > 0)
		{
			foreach (var userEntity in entity.UserEntityList.Where(l => !userEntityListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<UserEntityState>(x => x.Id == userEntity.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(userEntity).State = EntityState.Added;
				}
				else
				{
					Context.Entry(userEntity).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditCompanyCommandValidator : AbstractValidator<EditCompanyCommand>
{
    readonly ApplicationContext _context;

    public EditCompanyCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CompanyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Company with id {PropertyValue} does not exists");
        
    }
}
