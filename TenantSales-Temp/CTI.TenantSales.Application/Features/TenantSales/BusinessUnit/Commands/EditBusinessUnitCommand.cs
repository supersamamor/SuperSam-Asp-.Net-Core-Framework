using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Commands;

public record EditBusinessUnitCommand : BusinessUnitState, IRequest<Validation<Error, BusinessUnitState>>;

public class EditBusinessUnitCommandHandler : BaseCommandHandler<ApplicationContext, BusinessUnitState, EditBusinessUnitCommand>, IRequestHandler<EditBusinessUnitCommand, Validation<Error, BusinessUnitState>>
{
    public EditBusinessUnitCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBusinessUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BusinessUnitState>> Handle(EditBusinessUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditBusinessUnit(request, cancellationToken));


	public async Task<Validation<Error, BusinessUnitState>> EditBusinessUnit(EditBusinessUnitCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.BusinessUnit.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProjectBusinessUnitList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, BusinessUnitState>(entity);
	}
	
	private async Task UpdateProjectBusinessUnitList(BusinessUnitState entity, EditBusinessUnitCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectBusinessUnitState> projectBusinessUnitListForDeletion = new List<ProjectBusinessUnitState>();
		var queryProjectBusinessUnitForDeletion = Context.ProjectBusinessUnit.Where(l => l.BusinessUnitId == request.Id).AsNoTracking();
		if (entity.ProjectBusinessUnitList?.Count > 0)
		{
			queryProjectBusinessUnitForDeletion = queryProjectBusinessUnitForDeletion.Where(l => !(entity.ProjectBusinessUnitList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectBusinessUnitListForDeletion = await queryProjectBusinessUnitForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectBusinessUnit in projectBusinessUnitListForDeletion!)
		{
			Context.Entry(projectBusinessUnit).State = EntityState.Deleted;
		}
		if (entity.ProjectBusinessUnitList?.Count > 0)
		{
			foreach (var projectBusinessUnit in entity.ProjectBusinessUnitList.Where(l => !projectBusinessUnitListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectBusinessUnitState>(x => x.Id == projectBusinessUnit.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectBusinessUnit).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectBusinessUnit).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditBusinessUnitCommandValidator : AbstractValidator<EditBusinessUnitCommand>
{
    readonly ApplicationContext _context;

    public EditBusinessUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessUnit with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<BusinessUnitState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("BusinessUnit with name {PropertyValue} already exists");
	
    }
}
