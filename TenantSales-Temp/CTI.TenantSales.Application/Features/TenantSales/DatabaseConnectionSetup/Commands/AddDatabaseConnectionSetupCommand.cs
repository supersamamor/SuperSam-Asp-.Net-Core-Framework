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

namespace CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Commands;

public record AddDatabaseConnectionSetupCommand : DatabaseConnectionSetupState, IRequest<Validation<Error, DatabaseConnectionSetupState>>;

public class AddDatabaseConnectionSetupCommandHandler : BaseCommandHandler<ApplicationContext, DatabaseConnectionSetupState, AddDatabaseConnectionSetupCommand>, IRequestHandler<AddDatabaseConnectionSetupCommand, Validation<Error, DatabaseConnectionSetupState>>
{
    public AddDatabaseConnectionSetupCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddDatabaseConnectionSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DatabaseConnectionSetupState>> Handle(AddDatabaseConnectionSetupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddDatabaseConnectionSetup(request, cancellationToken));


	public async Task<Validation<Error, DatabaseConnectionSetupState>> AddDatabaseConnectionSetup(AddDatabaseConnectionSetupCommand request, CancellationToken cancellationToken)
	{
		DatabaseConnectionSetupState entity = Mapper.Map<DatabaseConnectionSetupState>(request);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DatabaseConnectionSetupState>(entity);
	}
	
	
	private async Task AddApprovers(string databaseConnectionSetupId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.DatabaseConnectionSetup).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = databaseConnectionSetupId,
				ApprovalList = new List<ApprovalState>()
			};
			foreach (var item in approverList)
			{
				var approval = new ApprovalState()
				{
					Sequence = item.Sequence,
					ApproverUserId = item.ApproverUserId,
				};
				if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
				{
					approval.EmailSendingStatus = SendingStatus.Pending;
				}
				approvalRecord.ApprovalList.Add(approval);
			}
			await Context.AddAsync(approvalRecord, cancellationToken);
		}
	}
}

public class AddDatabaseConnectionSetupCommandValidator : AbstractValidator<AddDatabaseConnectionSetupCommand>
{
    readonly ApplicationContext _context;

    public AddDatabaseConnectionSetupCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<DatabaseConnectionSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("DatabaseConnectionSetup with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<DatabaseConnectionSetupState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("DatabaseConnectionSetup with code {PropertyValue} already exists");
	RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<DatabaseConnectionSetupState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("DatabaseConnectionSetup with name {PropertyValue} already exists");
	
    }
}
