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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Tags.Commands;

public record AddTagsCommand : TagsState, IRequest<Validation<Error, TagsState>>;

public class AddTagsCommandHandler : BaseCommandHandler<ApplicationContext, TagsState, AddTagsCommand>, IRequestHandler<AddTagsCommand, Validation<Error, TagsState>>
{
	private readonly IdentityContext _identityContext;
    public AddTagsCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTagsCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, TagsState>> Handle(AddTagsCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddTags(request, cancellationToken));


	public async Task<Validation<Error, TagsState>> AddTags(AddTagsCommand request, CancellationToken cancellationToken)
	{
		TagsState entity = Mapper.Map<TagsState>(request);
		UpdateTaskTagList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TagsState>(entity);
	}
	
	private void UpdateTaskTagList(TagsState entity)
	{
		if (entity.TaskTagList?.Count > 0)
		{
			foreach (var taskTag in entity.TaskTagList!)
			{
				Context.Entry(taskTag).State = EntityState.Added;
			}
		}
	}
	
}

public class AddTagsCommandValidator : AbstractValidator<AddTagsCommand>
{
    readonly ApplicationContext _context;

    public AddTagsCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TagsState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Tags with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<TagsState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Tags with name {PropertyValue} already exists");
	
    }
}
