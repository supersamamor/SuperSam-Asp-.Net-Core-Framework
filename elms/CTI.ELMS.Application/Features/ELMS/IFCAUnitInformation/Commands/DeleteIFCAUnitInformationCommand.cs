using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Commands;

public record DeleteIFCAUnitInformationCommand : BaseCommand, IRequest<Validation<Error, IFCAUnitInformationState>>;

public class DeleteIFCAUnitInformationCommandHandler : BaseCommandHandler<ApplicationContext, IFCAUnitInformationState, DeleteIFCAUnitInformationCommand>, IRequestHandler<DeleteIFCAUnitInformationCommand, Validation<Error, IFCAUnitInformationState>>
{
    public DeleteIFCAUnitInformationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteIFCAUnitInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, IFCAUnitInformationState>> Handle(DeleteIFCAUnitInformationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteIFCAUnitInformationCommandValidator : AbstractValidator<DeleteIFCAUnitInformationCommand>
{
    readonly ApplicationContext _context;

    public DeleteIFCAUnitInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCAUnitInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAUnitInformation with id {PropertyValue} does not exists");
    }
}
