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

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Commands;

public record DeleteSampleParentCommand : BaseCommand, IRequest<Validation<Error, SampleParentState>>;

public class DeleteSampleParentCommandHandler : BaseCommandHandler<ApplicationContext, SampleParentState, DeleteSampleParentCommand>, IRequestHandler<DeleteSampleParentCommand, Validation<Error, SampleParentState>>
{
    public DeleteSampleParentCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSampleParentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SampleParentState>> Handle(DeleteSampleParentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSampleParentCommandValidator : AbstractValidator<DeleteSampleParentCommand>
{
    readonly ApplicationContext _context;

    public DeleteSampleParentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SampleParentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SampleParent with id {PropertyValue} does not exists");
    }
}
