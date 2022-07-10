using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Approval.Commands;

public record EditApproverSetupCommand : ApproverSetupState, IRequest<Validation<Error, ApproverSetupState>>;

public class EditApproverSetupCommandHandler : BaseCommandHandler<ApplicationContext, ApproverSetupState, EditApproverSetupCommand>, IRequestHandler<EditApproverSetupCommand, Validation<Error, ApproverSetupState>>
{
    public EditApproverSetupCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditApproverSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ApproverSetupState>> Handle(EditApproverSetupCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await EditApproverSetup(request, cancellationToken));


    public async Task<Validation<Error, ApproverSetupState>> EditApproverSetup(EditApproverSetupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApproverSetup.Where(l => l.Id == request.Id).SingleAsync(cancellationToken);
        _mapper.Map(request, entity);
        await UpdateApproverAssignmentList(entity, request, cancellationToken);
        _context.Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, ApproverSetupState>(entity);
    }

    private async Task UpdateApproverAssignmentList(ApproverSetupState entity, EditApproverSetupCommand request, CancellationToken cancellationToken)
    {
        IList<ApproverAssignmentState> approverAssignmentListForDeletion = new List<ApproverAssignmentState>();
        var queryApproverAssignmentForDeletion = _context.ApproverAssignment.Where(l => l.ApproverSetupId == request.Id).AsNoTracking();
        if (entity.ApproverAssignmentList?.Count > 0)
        {
            queryApproverAssignmentForDeletion = queryApproverAssignmentForDeletion.Where(l => !(entity.ApproverAssignmentList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        approverAssignmentListForDeletion = await queryApproverAssignmentForDeletion.ToListAsync(cancellationToken);
        foreach (var approverAssignment in approverAssignmentListForDeletion!)
        {
            _context.Entry(approverAssignment).State = EntityState.Deleted;
        }
        if (entity.ApproverAssignmentList?.Count > 0)
        {
            foreach (var approverAssignment in entity.ApproverAssignmentList.Where(l => !approverAssignmentListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await _context.NotExists<ApproverAssignmentState>(x => x.Id == approverAssignment.Id, cancellationToken: cancellationToken))
                {
                    _context.Entry(approverAssignment).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(approverAssignment).State = EntityState.Modified;
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
