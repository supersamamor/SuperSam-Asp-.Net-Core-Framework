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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.Project.Commands;

public record EditProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class EditProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, EditProjectCommand>, IRequestHandler<EditProjectCommand, Validation<Error, ProjectState>>
{
    public EditProjectCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectState>> Handle(EditProjectCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectCommandValidator : AbstractValidator<EditProjectCommand>
{
    readonly ApplicationContext _context;

    public EditProjectCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Project with id {PropertyValue} does not exists");
        
    }
}
