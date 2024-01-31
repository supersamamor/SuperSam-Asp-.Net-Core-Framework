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

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Commands;

public record DeleteContactInformationCommand : BaseCommand, IRequest<Validation<Error, ContactInformationState>>;

public class DeleteContactInformationCommandHandler : BaseCommandHandler<ApplicationContext, ContactInformationState, DeleteContactInformationCommand>, IRequestHandler<DeleteContactInformationCommand, Validation<Error, ContactInformationState>>
{
    public DeleteContactInformationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteContactInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ContactInformationState>> Handle(DeleteContactInformationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteContactInformationCommandValidator : AbstractValidator<DeleteContactInformationCommand>
{
    readonly ApplicationContext _context;

    public DeleteContactInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ContactInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ContactInformation with id {PropertyValue} does not exists");
    }
}
