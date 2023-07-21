using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;

public record EditPLEmployeeCommand : PLEmployeeState, IRequest<Validation<Error, PLEmployeeState>>;

public class EditPLEmployeeCommandHandler : BaseCommandHandler<ApplicationContext, PLEmployeeState, EditPLEmployeeCommand>, IRequestHandler<EditPLEmployeeCommand, Validation<Error, PLEmployeeState>>
{
    public EditPLEmployeeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPLEmployeeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PLEmployeeState>> Handle(EditPLEmployeeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditPLEmployee(request, cancellationToken));


	public async Task<Validation<Error, PLEmployeeState>> EditPLEmployee(EditPLEmployeeCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.PLEmployee.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdatePLContactInformationList(entity, request, cancellationToken);
		await UpdatePLHealthDeclarationList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, PLEmployeeState>(entity);
	}
	
	private async Task UpdatePLContactInformationList(PLEmployeeState entity, EditPLEmployeeCommand request, CancellationToken cancellationToken)
	{
		IList<PLContactInformationState> pLContactInformationListForDeletion = new List<PLContactInformationState>();
		var queryPLContactInformationForDeletion = Context.PLContactInformation.Where(l => l.PLEmployeeId == request.Id).AsNoTracking();
		if (entity.PLContactInformationList?.Count > 0)
		{
			queryPLContactInformationForDeletion = queryPLContactInformationForDeletion.Where(l => !(entity.PLContactInformationList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		pLContactInformationListForDeletion = await queryPLContactInformationForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var pLContactInformation in pLContactInformationListForDeletion!)
		{
			Context.Entry(pLContactInformation).State = EntityState.Deleted;
		}
		if (entity.PLContactInformationList?.Count > 0)
		{
			foreach (var pLContactInformation in entity.PLContactInformationList.Where(l => !pLContactInformationListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<PLContactInformationState>(x => x.Id == pLContactInformation.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(pLContactInformation).State = EntityState.Added;
				}
				else
				{
					Context.Entry(pLContactInformation).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdatePLHealthDeclarationList(PLEmployeeState entity, EditPLEmployeeCommand request, CancellationToken cancellationToken)
	{
		IList<PLHealthDeclarationState> pLHealthDeclarationListForDeletion = new List<PLHealthDeclarationState>();
		var queryPLHealthDeclarationForDeletion = Context.PLHealthDeclaration.Where(l => l.PLEmployeeId == request.Id).AsNoTracking();
		if (entity.PLHealthDeclarationList?.Count > 0)
		{
			queryPLHealthDeclarationForDeletion = queryPLHealthDeclarationForDeletion.Where(l => !(entity.PLHealthDeclarationList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		pLHealthDeclarationListForDeletion = await queryPLHealthDeclarationForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var pLHealthDeclaration in pLHealthDeclarationListForDeletion!)
		{
			Context.Entry(pLHealthDeclaration).State = EntityState.Deleted;
		}
		if (entity.PLHealthDeclarationList?.Count > 0)
		{
			foreach (var pLHealthDeclaration in entity.PLHealthDeclarationList.Where(l => !pLHealthDeclarationListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<PLHealthDeclarationState>(x => x.Id == pLHealthDeclaration.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(pLHealthDeclaration).State = EntityState.Added;
				}
				else
				{
					Context.Entry(pLHealthDeclaration).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditPLEmployeeCommandValidator : AbstractValidator<EditPLEmployeeCommand>
{
    readonly ApplicationContext _context;

    public EditPLEmployeeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PLEmployeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLEmployee with id {PropertyValue} does not exists");
        RuleFor(x => x.PLEmployeeCode).MustAsync(async (request, pLEmployeeCode, cancellation) => await _context.NotExists<PLEmployeeState>(x => x.PLEmployeeCode == pLEmployeeCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("PLEmployee with pLEmployeeCode {PropertyValue} already exists");
	
    }
}
