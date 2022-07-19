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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;

public record AddParentModuleCommand : ParentModuleState, IRequest<Validation<Error, ParentModuleState>>;

public class AddParentModuleCommandHandler : BaseCommandHandler<ApplicationContext, ParentModuleState, AddParentModuleCommand>, IRequestHandler<AddParentModuleCommand, Validation<Error, ParentModuleState>>
{
    public AddParentModuleCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddParentModuleCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ParentModuleState>> Handle(AddParentModuleCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddParentModule(request, cancellationToken));


	public async Task<Validation<Error, ParentModuleState>> AddParentModule(AddParentModuleCommand request, CancellationToken cancellationToken)
	{
		ParentModuleState entity = _mapper.Map<ParentModuleState>(request);
		_ = await _context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await _context.SaveChangesAsync(cancellationToken);
		return Success<Error, ParentModuleState>(entity);
	}
	
	
	private async Task AddApprovers(string parentModuleId, CancellationToken cancellationToken)
	{
		var approverList = await _context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.ParentModule).AsNoTracking().ToListAsync(cancellationToken);
		var approvalRecord = new ApprovalRecordState()
		{
			ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
			DataId = parentModuleId,
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
		await _context.AddAsync(approvalRecord, cancellationToken);
	}
}

public class AddParentModuleCommandValidator : AbstractValidator<AddParentModuleCommand>
{
    readonly ApplicationContext _context;

    public AddParentModuleCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ParentModuleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ParentModule with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<ParentModuleState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("ParentModule with name {PropertyValue} already exists");
	
    }
}
