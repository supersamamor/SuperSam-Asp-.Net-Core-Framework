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

public record EditTagsCommand : TagsState, IRequest<Validation<Error, TagsState>>;

public class EditTagsCommandHandler : BaseCommandHandler<ApplicationContext, TagsState, EditTagsCommand>, IRequestHandler<EditTagsCommand, Validation<Error, TagsState>>
{
    public EditTagsCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTagsCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TagsState>> Handle(EditTagsCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditTags(request, cancellationToken));


	public async Task<Validation<Error, TagsState>> EditTags(EditTagsCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Tags.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTaskTagList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TagsState>(entity);
	}
	
	private async Task UpdateTaskTagList(TagsState entity, EditTagsCommand request, CancellationToken cancellationToken)
	{
		IList<TaskTagState> taskTagListForDeletion = new List<TaskTagState>();
		var queryTaskTagForDeletion = Context.TaskTag.Where(l => l.TagId == request.Id).AsNoTracking();
		if (entity.TaskTagList?.Count > 0)
		{
			queryTaskTagForDeletion = queryTaskTagForDeletion.Where(l => !(entity.TaskTagList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		taskTagListForDeletion = await queryTaskTagForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var taskTag in taskTagListForDeletion!)
		{
			Context.Entry(taskTag).State = EntityState.Deleted;
		}
		if (entity.TaskTagList?.Count > 0)
		{
			foreach (var taskTag in entity.TaskTagList.Where(l => !taskTagListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TaskTagState>(x => x.Id == taskTag.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(taskTag).State = EntityState.Added;
				}
				else
				{
					Context.Entry(taskTag).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditTagsCommandValidator : AbstractValidator<EditTagsCommand>
{
    readonly ApplicationContext _context;

    public EditTagsCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TagsState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Tags with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<TagsState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Tags with name {PropertyValue} already exists");
	
    }
}
