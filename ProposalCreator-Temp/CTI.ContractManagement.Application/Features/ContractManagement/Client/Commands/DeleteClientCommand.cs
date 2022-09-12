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

namespace CTI.ContractManagement.Application.Features.ContractManagement.Client.Commands;

public record DeleteClientCommand : BaseCommand, IRequest<Validation<Error, ClientState>>;

public class DeleteClientCommandHandler : BaseCommandHandler<ApplicationContext, ClientState, DeleteClientCommand>, IRequestHandler<DeleteClientCommand, Validation<Error, ClientState>>
{
    public DeleteClientCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteClientCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ClientState>> Handle(DeleteClientCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
{
    readonly ApplicationContext _context;

    public DeleteClientCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ClientState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Client with id {PropertyValue} does not exists");
    }
}
