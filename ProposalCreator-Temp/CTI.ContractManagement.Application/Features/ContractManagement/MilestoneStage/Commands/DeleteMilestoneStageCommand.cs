using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Commands;

public record DeleteMilestoneStageCommand : BaseCommand, IRequest<Validation<Error, MilestoneStageState>>;

public class DeleteMilestoneStageCommandHandler : BaseCommandHandler<ApplicationContext, MilestoneStageState, DeleteMilestoneStageCommand>, IRequestHandler<DeleteMilestoneStageCommand, Validation<Error, MilestoneStageState>>
{
    public DeleteMilestoneStageCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteMilestoneStageCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MilestoneStageState>> Handle(DeleteMilestoneStageCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteMilestoneStageCommandValidator : AbstractValidator<DeleteMilestoneStageCommand>
{
    readonly ApplicationContext _context;

    public DeleteMilestoneStageCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MilestoneStageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MilestoneStage with id {PropertyValue} does not exists");
    }
}
