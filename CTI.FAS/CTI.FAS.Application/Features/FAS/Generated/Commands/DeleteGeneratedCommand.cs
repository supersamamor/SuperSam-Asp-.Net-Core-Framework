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

namespace CTI.FAS.Application.Features.FAS.Generated.Commands;

public record DeleteGeneratedCommand : BaseCommand, IRequest<Validation<Error, GeneratedState>>;

public class DeleteGeneratedCommandHandler : BaseCommandHandler<ApplicationContext, GeneratedState, DeleteGeneratedCommand>, IRequestHandler<DeleteGeneratedCommand, Validation<Error, GeneratedState>>
{
    public DeleteGeneratedCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteGeneratedCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, GeneratedState>> Handle(DeleteGeneratedCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteGeneratedCommandValidator : AbstractValidator<DeleteGeneratedCommand>
{
    readonly ApplicationContext _context;

    public DeleteGeneratedCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<GeneratedState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Generated with id {PropertyValue} does not exists");
    }
}
