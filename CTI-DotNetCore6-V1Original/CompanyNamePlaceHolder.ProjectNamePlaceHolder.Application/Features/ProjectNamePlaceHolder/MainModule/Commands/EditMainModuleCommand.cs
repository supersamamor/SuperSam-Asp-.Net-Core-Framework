using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;

public record EditMainModuleCommand : MainModuleState, IRequest<Validation<Error, MainModuleState>>;

public class EditMainModuleCommandHandler : BaseCommandHandler<ApplicationContext, MainModuleState, EditMainModuleCommand>, IRequestHandler<EditMainModuleCommand, Validation<Error, MainModuleState>>
{
    public EditMainModuleCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditMainModuleCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModuleState>> Handle(EditMainModuleCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditMainModule(request, cancellationToken));


	public async Task<Validation<Error, MainModuleState>> EditMainModule(EditMainModuleCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.MainModule.Where(l => l.Id == request.Id).SingleAsync();
		_mapper.Map(request, entity);
		await UpdateSubDetailListList(entity, request, cancellationToken);
		await UpdateSubDetailItemList(entity, request, cancellationToken);
		_context.Update(entity);
		_ = await _context.SaveChangesAsync(cancellationToken);
		return Success<Error, MainModuleState>(entity);
	}
	
	private async Task UpdateSubDetailListList(MainModuleState entity, EditMainModuleCommand request, CancellationToken cancellationToken)
	{
		IList<SubDetailListState> subDetailListListForDeletion = new List<SubDetailListState>();
		var querySubDetailListForDeletion = _context.SubDetailList.Where(l => l.TestForeignKeyOne == request.Id).AsNoTracking();
		if (entity.SubDetailListList?.Count > 0)
		{
			querySubDetailListForDeletion = querySubDetailListForDeletion.Where(l => !(entity.SubDetailListList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		subDetailListListForDeletion = await querySubDetailListForDeletion.ToListAsync();
		foreach (var subDetailList in subDetailListListForDeletion!)
		{
			_context.Entry(subDetailList).State = EntityState.Deleted;
		}
		if (entity.SubDetailListList?.Count > 0)
		{
			foreach (var subDetailList in entity.SubDetailListList.Where(l => !subDetailListListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await _context.NotExists<SubDetailListState>(x => x.Id == subDetailList.Id, cancellationToken: cancellationToken))
				{
					_context.Entry(subDetailList).State = EntityState.Added;
				}
				else
				{
					_context.Entry(subDetailList).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateSubDetailItemList(MainModuleState entity, EditMainModuleCommand request, CancellationToken cancellationToken)
	{
		IList<SubDetailItemState> subDetailItemListForDeletion = new List<SubDetailItemState>();
		var querySubDetailItemForDeletion = _context.SubDetailItem.Where(l => l.TestForeignKeyTwo == request.Id).AsNoTracking();
		if (entity.SubDetailItemList?.Count > 0)
		{
			querySubDetailItemForDeletion = querySubDetailItemForDeletion.Where(l => !(entity.SubDetailItemList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		subDetailItemListForDeletion = await querySubDetailItemForDeletion.ToListAsync();
		foreach (var subDetailItem in subDetailItemListForDeletion!)
		{
			_context.Entry(subDetailItem).State = EntityState.Deleted;
		}
		if (entity.SubDetailItemList?.Count > 0)
		{
			foreach (var subDetailItem in entity.SubDetailItemList.Where(l => !subDetailItemListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await _context.NotExists<SubDetailItemState>(x => x.Id == subDetailItem.Id, cancellationToken: cancellationToken))
				{
					_context.Entry(subDetailItem).State = EntityState.Added;
				}
				else
				{
					_context.Entry(subDetailItem).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditMainModuleCommandValidator : AbstractValidator<EditMainModuleCommand>
{
    readonly ApplicationContext _context;

    public EditMainModuleCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MainModuleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModule with id {PropertyValue} does not exists");
        
    }
}
