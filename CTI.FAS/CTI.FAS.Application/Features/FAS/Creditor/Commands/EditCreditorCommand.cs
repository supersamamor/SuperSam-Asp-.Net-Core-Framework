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

namespace CTI.FAS.Application.Features.FAS.Creditor.Commands;

public record EditCreditorCommand : CreditorState, IRequest<Validation<Error, CreditorState>>;

public class EditCreditorCommandHandler : BaseCommandHandler<ApplicationContext, CreditorState, EditCreditorCommand>, IRequestHandler<EditCreditorCommand, Validation<Error, CreditorState>>
{
    public EditCreditorCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCreditorCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CreditorState>> Handle(EditCreditorCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditCreditor(request, cancellationToken));


	public async Task<Validation<Error, CreditorState>> EditCreditor(EditCreditorCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Creditor.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateCheckReleaseOptionList(entity, request, cancellationToken);
		await UpdateCreditorEmailList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CreditorState>(entity);
	}
	
	private async Task UpdateCheckReleaseOptionList(CreditorState entity, EditCreditorCommand request, CancellationToken cancellationToken)
	{
		IList<CheckReleaseOptionState> checkReleaseOptionListForDeletion = new List<CheckReleaseOptionState>();
		var queryCheckReleaseOptionForDeletion = Context.CheckReleaseOption.Where(l => l.CreditorId == request.Id).AsNoTracking();
		if (entity.CheckReleaseOptionList?.Count > 0)
		{
			queryCheckReleaseOptionForDeletion = queryCheckReleaseOptionForDeletion.Where(l => !(entity.CheckReleaseOptionList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		checkReleaseOptionListForDeletion = await queryCheckReleaseOptionForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var checkReleaseOption in checkReleaseOptionListForDeletion!)
		{
			Context.Entry(checkReleaseOption).State = EntityState.Deleted;
		}
		if (entity.CheckReleaseOptionList?.Count > 0)
		{
			foreach (var checkReleaseOption in entity.CheckReleaseOptionList.Where(l => !checkReleaseOptionListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<CheckReleaseOptionState>(x => x.Id == checkReleaseOption.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(checkReleaseOption).State = EntityState.Added;
				}
				else
				{
					Context.Entry(checkReleaseOption).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateCreditorEmailList(CreditorState entity, EditCreditorCommand request, CancellationToken cancellationToken)
	{
		IList<CreditorEmailState> creditorEmailListForDeletion = new List<CreditorEmailState>();
		var queryCreditorEmailForDeletion = Context.CreditorEmail.Where(l => l.CreditorId == request.Id).AsNoTracking();
		if (entity.CreditorEmailList?.Count > 0)
		{
			queryCreditorEmailForDeletion = queryCreditorEmailForDeletion.Where(l => !(entity.CreditorEmailList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		creditorEmailListForDeletion = await queryCreditorEmailForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var creditorEmail in creditorEmailListForDeletion!)
		{
			Context.Entry(creditorEmail).State = EntityState.Deleted;
		}
		if (entity.CreditorEmailList?.Count > 0)
		{
			foreach (var creditorEmail in entity.CreditorEmailList.Where(l => !creditorEmailListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<CreditorEmailState>(x => x.Id == creditorEmail.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(creditorEmail).State = EntityState.Added;
				}
				else
				{
					Context.Entry(creditorEmail).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditCreditorCommandValidator : AbstractValidator<EditCreditorCommand>
{
    readonly ApplicationContext _context;

    public EditCreditorCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CreditorState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Creditor with id {PropertyValue} does not exists");
        
    }
}
