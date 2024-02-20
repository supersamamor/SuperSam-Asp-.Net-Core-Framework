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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Commands;

public record DeleteApplicationConfigurationCommand : BaseCommand, IRequest<Validation<Error, ApplicationConfigurationState>>;

public class DeleteApplicationConfigurationCommandHandler : BaseCommandHandler<ApplicationContext, ApplicationConfigurationState, DeleteApplicationConfigurationCommand>, IRequestHandler<DeleteApplicationConfigurationCommand, Validation<Error, ApplicationConfigurationState>>
{
    public DeleteApplicationConfigurationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteApplicationConfigurationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ApplicationConfigurationState>> Handle(DeleteApplicationConfigurationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteApplicationConfigurationCommandValidator : AbstractValidator<DeleteApplicationConfigurationCommand>
{
    readonly ApplicationContext _context;

    public DeleteApplicationConfigurationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ApplicationConfigurationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ApplicationConfiguration with id {PropertyValue} does not exists");
    }
}
