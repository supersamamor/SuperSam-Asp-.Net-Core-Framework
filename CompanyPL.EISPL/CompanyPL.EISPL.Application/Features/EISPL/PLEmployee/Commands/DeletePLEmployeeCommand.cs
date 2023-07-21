using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;

public record DeletePLEmployeeCommand : BaseCommand, IRequest<Validation<Error, PLEmployeeState>>;

public class DeletePLEmployeeCommandHandler : BaseCommandHandler<ApplicationContext, PLEmployeeState, DeletePLEmployeeCommand>, IRequestHandler<DeletePLEmployeeCommand, Validation<Error, PLEmployeeState>>
{
    public DeletePLEmployeeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePLEmployeeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PLEmployeeState>> Handle(DeletePLEmployeeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePLEmployeeCommandValidator : AbstractValidator<DeletePLEmployeeCommand>
{
    readonly ApplicationContext _context;

    public DeletePLEmployeeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PLEmployeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLEmployee with id {PropertyValue} does not exists");
    }
}
