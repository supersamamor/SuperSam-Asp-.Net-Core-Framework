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

public record EditDepartmentCommand : DepartmentState, IRequest<Validation<Error, DepartmentState>>;

public class EditDepartmentCommandHandler : BaseCommandHandler<ApplicationContext, DepartmentState, EditDepartmentCommand>, IRequestHandler<EditDepartmentCommand, Validation<Error, DepartmentState>>
{
    public EditDepartmentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDepartmentCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, DepartmentState>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditDepartmentCommandValidator : AbstractValidator<EditDepartmentCommand>
{
    readonly ApplicationContext _context;

    public EditDepartmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DepartmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Department with id {PropertyValue} does not exists");
        
    }
}
