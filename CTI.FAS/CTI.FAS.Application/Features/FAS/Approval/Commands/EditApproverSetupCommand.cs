using AutoMapper;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.Approval.Commands;

public record EditApproverSetupCommand : ApproverSetupState, IRequest<Validation<Error, ApproverSetupState>>;

public class EditApproverSetupCommandHandler : BaseCommandHandler<ApplicationContext, ApproverSetupState, EditApproverSetupCommand>, IRequestHandler<EditApproverSetupCommand, Validation<Error, ApproverSetupState>>
{
    public EditApproverSetupCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditApproverSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ApproverSetupState>> Handle(EditApproverSetupCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await EditApproverSetup(request, cancellationToken));


    public async Task<Validation<Error, ApproverSetupState>> EditApproverSetup(EditApproverSetupCommand request, CancellationToken cancellationToken)
    {
        var entity = await Context.ApproverSetup.Where(l => l.Id == request.Id).SingleAsync(cancellationToken);
        Mapper.Map(request, entity);
        await UpdateApproverAssignmentList(entity, request, cancellationToken);
        Context.Update(entity);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, ApproverSetupState>(entity);
    }

    private async Task UpdateApproverAssignmentList(ApproverSetupState entity, EditApproverSetupCommand request, CancellationToken cancellationToken)
    {
        IList<ApproverAssignmentState> approverAssignmentListForDeletion = new List<ApproverAssignmentState>();
        var queryApproverAssignmentForDeletion = Context.ApproverAssignment.Where(l => l.ApproverSetupId == request.Id).AsNoTracking();
        if (entity.ApproverAssignmentList?.Count > 0)
        {
            queryApproverAssignmentForDeletion = queryApproverAssignmentForDeletion.Where(l => !(entity.ApproverAssignmentList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        approverAssignmentListForDeletion = await queryApproverAssignmentForDeletion.ToListAsync(cancellationToken);
        foreach (var approverAssignment in approverAssignmentListForDeletion!)
        {
            Context.Entry(approverAssignment).State = EntityState.Deleted;
        }
        if (entity.ApproverAssignmentList?.Count > 0)
        {
            foreach (var approverAssignment in entity.ApproverAssignmentList.Where(l => !approverAssignmentListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await Context.NotExists<ApproverAssignmentState>(x => x.Id == approverAssignment.Id, cancellationToken: cancellationToken))
                {
                    Context.Entry(approverAssignment).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(approverAssignment).State = EntityState.Modified;
                }
            }
        }
    }

}

public class EditApproverSetupCommandValidator : AbstractValidator<EditApproverSetupCommand>
{
    readonly ApplicationContext _context;

    public EditApproverSetupCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ApproverSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ApproverSetup with id {PropertyValue} does not exists");
        RuleFor(x => new { x.TableName, x.Entity }).MustAsync(async (request, tableObject, cancellation) => await _context.NotExists<ApproverSetupState>(x => x.TableName == tableObject.TableName && x.Entity == tableObject.Entity && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ApproverSetup with tableName {PropertyValue} already exists");
    }
}
