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

namespace CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;

public record DeleteTestCommand : BaseCommand, IRequest<Validation<Error, TestState>>;

public class DeleteTestCommandHandler : BaseCommandHandler<ApplicationContext, TestState, DeleteTestCommand>, IRequestHandler<DeleteTestCommand, Validation<Error, TestState>>
{
    public DeleteTestCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTestCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TestState>> Handle(DeleteTestCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTestCommandValidator : AbstractValidator<DeleteTestCommand>
{
    readonly ApplicationContext _context;

    public DeleteTestCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TestState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Test with id {PropertyValue} does not exists");
    }
}
