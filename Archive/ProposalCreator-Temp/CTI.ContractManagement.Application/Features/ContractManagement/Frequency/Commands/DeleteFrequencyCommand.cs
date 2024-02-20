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

namespace CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Commands;

public record DeleteFrequencyCommand : BaseCommand, IRequest<Validation<Error, FrequencyState>>;

public class DeleteFrequencyCommandHandler : BaseCommandHandler<ApplicationContext, FrequencyState, DeleteFrequencyCommand>, IRequestHandler<DeleteFrequencyCommand, Validation<Error, FrequencyState>>
{
    public DeleteFrequencyCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteFrequencyCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, FrequencyState>> Handle(DeleteFrequencyCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteFrequencyCommandValidator : AbstractValidator<DeleteFrequencyCommand>
{
    readonly ApplicationContext _context;

    public DeleteFrequencyCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FrequencyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Frequency with id {PropertyValue} does not exists");
    }
}
