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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;

public record EditMainModulePlaceHolderCommand : MainModulePlaceHolderState, IRequest<Validation<Error, MainModulePlaceHolderState>>;

public class EditMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, MainModulePlaceHolderState, EditMainModulePlaceHolderCommand>, IRequestHandler<EditMainModulePlaceHolderCommand, Validation<Error, MainModulePlaceHolderState>>
{
    public EditMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditMainModulePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModulePlaceHolderState>> Handle(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditMainModulePlaceHolder(request, cancellationToken));


	public async Task<Validation<Error, MainModulePlaceHolderState>> EditMainModulePlaceHolder(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.MainModulePlaceHolder.Where(l => l.Id == request.Id).SingleAsync();
		_mapper.Map(request, entity);
		await UpdateSubDetailItemPlaceHolderList(entity, request, cancellationToken);
		await UpdateSubDetailListPlaceHolderList(entity, request, cancellationToken);
		_context.Update(entity);
		_ = await _context.SaveChangesAsync(cancellationToken);
		return Success<Error, MainModulePlaceHolderState>(entity);
	}
	
	private async Task UpdateSubDetailItemPlaceHolderList(MainModulePlaceHolderState entity, EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
	{
		IList<SubDetailItemPlaceHolderState> subDetailItemPlaceHolderListForDeletion = new List<SubDetailItemPlaceHolderState>();
		var querySubDetailItemPlaceHolderForDeletion = _context.SubDetailItemPlaceHolder.Where(l => l.MainModulePlaceHolderId == request.Id).AsNoTracking();
		if (entity.SubDetailItemPlaceHolderList?.Count > 0)
		{
			querySubDetailItemPlaceHolderForDeletion = querySubDetailItemPlaceHolderForDeletion.Where(l => !(entity.SubDetailItemPlaceHolderList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		subDetailItemPlaceHolderListForDeletion = await querySubDetailItemPlaceHolderForDeletion.ToListAsync();
		foreach (var subDetailItemPlaceHolder in subDetailItemPlaceHolderListForDeletion!)
		{
			_context.Entry(subDetailItemPlaceHolder).State = EntityState.Deleted;
		}
		if (entity.SubDetailItemPlaceHolderList?.Count > 0)
		{
			foreach (var subDetailItemPlaceHolder in entity.SubDetailItemPlaceHolderList.Where(l => !subDetailItemPlaceHolderListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await _context.NotExists<SubDetailItemPlaceHolderState>(x => x.Id == subDetailItemPlaceHolder.Id, cancellationToken: cancellationToken))
				{
					_context.Entry(subDetailItemPlaceHolder).State = EntityState.Added;
				}
				else
				{
					_context.Entry(subDetailItemPlaceHolder).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateSubDetailListPlaceHolderList(MainModulePlaceHolderState entity, EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
	{
		IList<SubDetailListPlaceHolderState> subDetailListPlaceHolderListForDeletion = new List<SubDetailListPlaceHolderState>();
		var querySubDetailListPlaceHolderForDeletion = _context.SubDetailListPlaceHolder.Where(l => l.MainModulePlaceHolderId == request.Id).AsNoTracking();
		if (entity.SubDetailListPlaceHolderList?.Count > 0)
		{
			querySubDetailListPlaceHolderForDeletion = querySubDetailListPlaceHolderForDeletion.Where(l => !(entity.SubDetailListPlaceHolderList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		subDetailListPlaceHolderListForDeletion = await querySubDetailListPlaceHolderForDeletion.ToListAsync();
		foreach (var subDetailListPlaceHolder in subDetailListPlaceHolderListForDeletion!)
		{
			_context.Entry(subDetailListPlaceHolder).State = EntityState.Deleted;
		}
		if (entity.SubDetailListPlaceHolderList?.Count > 0)
		{
			foreach (var subDetailListPlaceHolder in entity.SubDetailListPlaceHolderList.Where(l => !subDetailListPlaceHolderListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await _context.NotExists<SubDetailListPlaceHolderState>(x => x.Id == subDetailListPlaceHolder.Id, cancellationToken: cancellationToken))
				{
					_context.Entry(subDetailListPlaceHolder).State = EntityState.Added;
				}
				else
				{
					_context.Entry(subDetailListPlaceHolder).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditMainModulePlaceHolderCommandValidator : AbstractValidator<EditMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditMainModulePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MainModulePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModulePlaceHolder with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<MainModulePlaceHolderState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("MainModulePlaceHolder with code {PropertyValue} already exists");
	
    }
}
