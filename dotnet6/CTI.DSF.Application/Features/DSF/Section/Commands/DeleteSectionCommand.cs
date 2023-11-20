using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.DSF.Application.Features.DSF.Section.Commands;

public record DeleteSectionCommand : BaseCommand, IRequest<Validation<Error, SectionState>>;

public class DeleteSectionCommandHandler : BaseCommandHandler<ApplicationContext, SectionState, DeleteSectionCommand>, IRequestHandler<DeleteSectionCommand, Validation<Error, SectionState>>
{
    public DeleteSectionCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSectionCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SectionState>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSectionCommandValidator : AbstractValidator<DeleteSectionCommand>
{
    readonly ApplicationContext _context;

    public DeleteSectionCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SectionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Section with id {PropertyValue} does not exists");
    }
}
