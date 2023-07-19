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

namespace CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Commands;

public record DeletePLContactInformationCommand : BaseCommand, IRequest<Validation<Error, PLContactInformationState>>;

public class DeletePLContactInformationCommandHandler : BaseCommandHandler<ApplicationContext, PLContactInformationState, DeletePLContactInformationCommand>, IRequestHandler<DeletePLContactInformationCommand, Validation<Error, PLContactInformationState>>
{
    public DeletePLContactInformationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePLContactInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PLContactInformationState>> Handle(DeletePLContactInformationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePLContactInformationCommandValidator : AbstractValidator<DeletePLContactInformationCommand>
{
    readonly ApplicationContext _context;

    public DeletePLContactInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PLContactInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLContactInformation with id {PropertyValue} does not exists");
    }
}
