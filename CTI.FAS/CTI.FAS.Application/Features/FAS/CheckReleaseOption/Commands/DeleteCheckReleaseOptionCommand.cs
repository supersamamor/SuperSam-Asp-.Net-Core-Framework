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

namespace CTI.FAS.Application.Features.FAS.CheckReleaseOption.Commands;

public record DeleteCheckReleaseOptionCommand : BaseCommand, IRequest<Validation<Error, CheckReleaseOptionState>>;

public class DeleteCheckReleaseOptionCommandHandler : BaseCommandHandler<ApplicationContext, CheckReleaseOptionState, DeleteCheckReleaseOptionCommand>, IRequestHandler<DeleteCheckReleaseOptionCommand, Validation<Error, CheckReleaseOptionState>>
{
    public DeleteCheckReleaseOptionCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCheckReleaseOptionCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CheckReleaseOptionState>> Handle(DeleteCheckReleaseOptionCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCheckReleaseOptionCommandValidator : AbstractValidator<DeleteCheckReleaseOptionCommand>
{
    readonly ApplicationContext _context;

    public DeleteCheckReleaseOptionCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CheckReleaseOptionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CheckReleaseOption with id {PropertyValue} does not exists");
    }
}
