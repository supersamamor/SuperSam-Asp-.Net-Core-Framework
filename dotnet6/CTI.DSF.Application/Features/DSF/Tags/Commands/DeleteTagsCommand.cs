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

namespace CTI.DSF.Application.Features.DSF.Tags.Commands;

public record DeleteTagsCommand : BaseCommand, IRequest<Validation<Error, TagsState>>;

public class DeleteTagsCommandHandler : BaseCommandHandler<ApplicationContext, TagsState, DeleteTagsCommand>, IRequestHandler<DeleteTagsCommand, Validation<Error, TagsState>>
{
    public DeleteTagsCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTagsCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TagsState>> Handle(DeleteTagsCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTagsCommandValidator : AbstractValidator<DeleteTagsCommand>
{
    readonly ApplicationContext _context;

    public DeleteTagsCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TagsState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Tags with id {PropertyValue} does not exists");
    }
}
