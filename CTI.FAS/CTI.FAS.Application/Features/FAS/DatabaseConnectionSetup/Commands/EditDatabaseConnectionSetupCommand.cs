using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.DatabaseConnectionSetup.Commands;

public record EditDatabaseConnectionSetupCommand : DatabaseConnectionSetupState, IRequest<Validation<Error, DatabaseConnectionSetupState>>;

public class EditDatabaseConnectionSetupCommandHandler : BaseCommandHandler<ApplicationContext, DatabaseConnectionSetupState, EditDatabaseConnectionSetupCommand>, IRequestHandler<EditDatabaseConnectionSetupCommand, Validation<Error, DatabaseConnectionSetupState>>
{
    public EditDatabaseConnectionSetupCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDatabaseConnectionSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, DatabaseConnectionSetupState>> Handle(EditDatabaseConnectionSetupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditDatabaseConnectionSetupCommandValidator : AbstractValidator<EditDatabaseConnectionSetupCommand>
{
    readonly ApplicationContext _context;

    public EditDatabaseConnectionSetupCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DatabaseConnectionSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("DatabaseConnectionSetup with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<DatabaseConnectionSetupState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("DatabaseConnectionSetup with code {PropertyValue} already exists");
	RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<DatabaseConnectionSetupState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("DatabaseConnectionSetup with name {PropertyValue} already exists");
	
    }
}
