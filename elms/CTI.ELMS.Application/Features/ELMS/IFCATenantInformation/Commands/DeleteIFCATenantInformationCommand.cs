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

namespace CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Commands;

public record DeleteIFCATenantInformationCommand : BaseCommand, IRequest<Validation<Error, IFCATenantInformationState>>;

public class DeleteIFCATenantInformationCommandHandler : BaseCommandHandler<ApplicationContext, IFCATenantInformationState, DeleteIFCATenantInformationCommand>, IRequestHandler<DeleteIFCATenantInformationCommand, Validation<Error, IFCATenantInformationState>>
{
    public DeleteIFCATenantInformationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteIFCATenantInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, IFCATenantInformationState>> Handle(DeleteIFCATenantInformationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteIFCATenantInformationCommandValidator : AbstractValidator<DeleteIFCATenantInformationCommand>
{
    readonly ApplicationContext _context;

    public DeleteIFCATenantInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCATenantInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCATenantInformation with id {PropertyValue} does not exists");
    }
}
