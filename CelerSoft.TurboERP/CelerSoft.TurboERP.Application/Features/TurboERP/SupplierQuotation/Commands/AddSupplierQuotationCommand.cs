using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Commands;

public record AddSupplierQuotationCommand : SupplierQuotationState, IRequest<Validation<Error, SupplierQuotationState>>;

public class AddSupplierQuotationCommandHandler : BaseCommandHandler<ApplicationContext, SupplierQuotationState, AddSupplierQuotationCommand>, IRequestHandler<AddSupplierQuotationCommand, Validation<Error, SupplierQuotationState>>
{
	private readonly IdentityContext _identityContext;
    public AddSupplierQuotationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSupplierQuotationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, SupplierQuotationState>> Handle(AddSupplierQuotationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddSupplierQuotation(request, cancellationToken));


	public async Task<Validation<Error, SupplierQuotationState>> AddSupplierQuotation(AddSupplierQuotationCommand request, CancellationToken cancellationToken)
	{
		SupplierQuotationState entity = Mapper.Map<SupplierQuotationState>(request);
		UpdateSupplierQuotationItemList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SupplierQuotationState>(entity);
	}
	
	private void UpdateSupplierQuotationItemList(SupplierQuotationState entity)
	{
		if (entity.SupplierQuotationItemList?.Count > 0)
		{
			foreach (var supplierQuotationItem in entity.SupplierQuotationItemList!)
			{
				Context.Entry(supplierQuotationItem).State = EntityState.Added;
			}
		}
	}
	
	private async Task AddApprovers(string supplierQuotationId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.SupplierQuotation).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = supplierQuotationId,
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

public class AddSupplierQuotationCommandValidator : AbstractValidator<AddSupplierQuotationCommand>
{
    readonly ApplicationContext _context;

    public AddSupplierQuotationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SupplierQuotationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierQuotation with id {PropertyValue} already exists");
        
    }
}
