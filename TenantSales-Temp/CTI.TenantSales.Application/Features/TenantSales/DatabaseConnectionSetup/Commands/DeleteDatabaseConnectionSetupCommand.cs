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

namespace CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Commands;

public record DeleteDatabaseConnectionSetupCommand : BaseCommand, IRequest<Validation<Error, DatabaseConnectionSetupState>>;

public class DeleteDatabaseConnectionSetupCommandHandler : BaseCommandHandler<ApplicationContext, DatabaseConnectionSetupState, DeleteDatabaseConnectionSetupCommand>, IRequestHandler<DeleteDatabaseConnectionSetupCommand, Validation<Error, DatabaseConnectionSetupState>>
{
    public DeleteDatabaseConnectionSetupCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteDatabaseConnectionSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DatabaseConnectionSetupState>> Handle(DeleteDatabaseConnectionSetupCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteDatabaseConnectionSetupCommandValidator : AbstractValidator<DeleteDatabaseConnectionSetupCommand>
{
    readonly ApplicationContext _context;

    public DeleteDatabaseConnectionSetupCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DatabaseConnectionSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("DatabaseConnectionSetup with id {PropertyValue} does not exists");
    }
}
