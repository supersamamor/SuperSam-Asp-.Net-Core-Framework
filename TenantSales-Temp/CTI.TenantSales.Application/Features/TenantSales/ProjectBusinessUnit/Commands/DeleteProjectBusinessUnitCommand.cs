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

namespace CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Commands;

public record DeleteProjectBusinessUnitCommand : BaseCommand, IRequest<Validation<Error, ProjectBusinessUnitState>>;

public class DeleteProjectBusinessUnitCommandHandler : BaseCommandHandler<ApplicationContext, ProjectBusinessUnitState, DeleteProjectBusinessUnitCommand>, IRequestHandler<DeleteProjectBusinessUnitCommand, Validation<Error, ProjectBusinessUnitState>>
{
    public DeleteProjectBusinessUnitCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectBusinessUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectBusinessUnitState>> Handle(DeleteProjectBusinessUnitCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectBusinessUnitCommandValidator : AbstractValidator<DeleteProjectBusinessUnitCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectBusinessUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectBusinessUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectBusinessUnit with id {PropertyValue} does not exists");
    }
}
