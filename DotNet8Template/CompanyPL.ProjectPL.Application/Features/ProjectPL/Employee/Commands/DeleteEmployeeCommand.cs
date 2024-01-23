using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Commands;

public record DeleteEmployeeCommand : BaseCommand, IRequest<Validation<Error, EmployeeState>>;

public class DeleteEmployeeCommandHandler : BaseCommandHandler<ApplicationContext, EmployeeState, DeleteEmployeeCommand>, IRequestHandler<DeleteEmployeeCommand, Validation<Error, EmployeeState>>
{
    public DeleteEmployeeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteEmployeeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, EmployeeState>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    readonly ApplicationContext _context;

    public DeleteEmployeeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EmployeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Employee with id {PropertyValue} does not exists");
    }
}
