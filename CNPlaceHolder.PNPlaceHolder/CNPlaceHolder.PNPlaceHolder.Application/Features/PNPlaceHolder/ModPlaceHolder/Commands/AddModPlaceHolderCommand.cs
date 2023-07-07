using AutoMapper;
using CNPlaceHolder.Common.Core.Commands;
using CNPlaceHolder.Common.Data;
using CNPlaceHolder.Common.Utility.Validators;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;

public record AddModPlaceHolderCommand : ModPlaceHolderState, IRequest<Validation<Error, ModPlaceHolderState>>;

public class AddModPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, ModPlaceHolderState, AddModPlaceHolderCommand>, IRequestHandler<AddModPlaceHolderCommand, Validation<Error, ModPlaceHolderState>>
{
	private readonly IdentityContext _identityContext;
    public AddModPlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddModPlaceHolderCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ModPlaceHolderState>> Handle(AddModPlaceHolderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddModPlaceHolder(request, cancellationToken));


	public async Task<Validation<Error, ModPlaceHolderState>> AddModPlaceHolder(AddModPlaceHolderCommand request, CancellationToken cancellationToken)
	{
		ModPlaceHolderState entity = Mapper.Map<ModPlaceHolderState>(request);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ModPlaceHolderState>(entity);
	}
	
	
	private async Task AddApprovers(string modPlaceHolderId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.ModPlaceHolder).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = modPlaceHolderId,
				ApprovalList = new List<ApprovalState>()
			};
			foreach (var approverItem in approverList)
			{
				if (approverItem.ApproverType == ApproverTypes.User)
				{
					var approval = new ApprovalState()
					{
						Sequence = approverItem.Sequence,
						ApproverUserId = approverItem.ApproverUserId!,
					};
					if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
					{
						approval.EmailSendingStatus = SendingStatus.Pending;
					}
					approvalRecord.ApprovalList.Add(approval);
				}
				else if (approverItem.ApproverType == ApproverTypes.Role)
				{
					var userListWithRole = await (from a in _identityContext.Users
													join b in _identityContext.UserRoles on a.Id equals b.UserId
													join c in _identityContext.Roles on b.RoleId equals c.Id
													where c.Id == approverItem.ApproverRoleId
													select a.Id).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
					foreach (var userId in userListWithRole)
					{
						var approval = new ApprovalState()
						{
							Sequence = approverItem.Sequence,
							ApproverUserId = userId,
						};
						if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
						{
							approval.EmailSendingStatus = SendingStatus.Pending;
						}
						approvalRecord.ApprovalList.Add(approval);
					}
				}
			}
			await Context.AddAsync(approvalRecord, cancellationToken);
		}
	}
}

public class AddModPlaceHolderCommandValidator : AbstractValidator<AddModPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddModPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ModPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ModPlaceHolder with id {PropertyValue} already exists");
        RuleFor(x => x.ColPlaceHolder).MustAsync(async (colPlaceHolder, cancellation) => await _context.NotExists<ModPlaceHolderState>(x => x.ColPlaceHolder == colPlaceHolder, cancellationToken: cancellation)).WithMessage("ModPlaceHolder with colPlaceHolder {PropertyValue} already exists");
	
    }
}
