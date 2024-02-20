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

namespace CTI.DSF.Application.Features.DSF.Department.Commands;

public record DeleteDepartmentCommand : BaseCommand, IRequest<Validation<Error, DepartmentState>>;

public class DeleteDepartmentCommandHandler : BaseCommandHandler<ApplicationContext, DepartmentState, DeleteDepartmentCommand>, IRequestHandler<DeleteDepartmentCommand, Validation<Error, DepartmentState>>
{
    public DeleteDepartmentCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteDepartmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DepartmentState>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    readonly ApplicationContext _context;

    public DeleteDepartmentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DepartmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Department with id {PropertyValue} does not exists");
    }
}
