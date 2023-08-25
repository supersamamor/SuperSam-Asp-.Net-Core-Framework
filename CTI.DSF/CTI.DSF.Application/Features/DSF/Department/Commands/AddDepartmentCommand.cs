using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Department.Commands;

public record AddDepartmentCommand : DepartmentState, IRequest<Validation<Error, DepartmentState>>;

public class AddDepartmentCommandHandler : BaseCommandHandler<ApplicationContext, DepartmentState, AddDepartmentCommand>, IRequestHandler<AddDepartmentCommand, Validation<Error, DepartmentState>>
{
	private readonly IdentityContext _identityContext;
    public AddDepartmentCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddDepartmentCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, DepartmentState>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddDepartmentCommandValidator : AbstractValidator<AddDepartmentCommand>
{
    readonly ApplicationContext _context;

    public AddDepartmentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<DepartmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Department with id {PropertyValue} already exists");
        
    }
}
