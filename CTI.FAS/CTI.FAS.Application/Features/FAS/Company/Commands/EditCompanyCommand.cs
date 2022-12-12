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
		await UpdateBankList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CompanyState>(entity);
	}
	private async Task UpdateBankList(CompanyState entity, EditCompanyCommand request, CancellationToken cancellationToken)
	{
		IList<BankState> bankListForDeletion = new List<BankState>();
		var queryBankForDeletion = Context.Bank.Where(l => l.CompanyId == request.Id).AsNoTracking();
		if (entity.BankList?.Count > 0)
		{
			queryBankForDeletion = queryBankForDeletion.Where(l => !(entity.BankList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		bankListForDeletion = await queryBankForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var bank in bankListForDeletion!)
		{
			Context.Entry(bank).State = EntityState.Deleted;
		}
		if (entity.BankList?.Count > 0)
		{
			foreach (var bank in entity.BankList.Where(l => !bankListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<BankState>(x => x.Id == bank.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(bank).State = EntityState.Added;
				}
				else
				{
					Context.Entry(bank).State = EntityState.Modified;
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
