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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Commands;

public record EditSampleParentCommand : SampleParentState, IRequest<Validation<Error, SampleParentState>>;

public class EditSampleParentCommandHandler : BaseCommandHandler<ApplicationContext, SampleParentState, EditSampleParentCommand>, IRequestHandler<EditSampleParentCommand, Validation<Error, SampleParentState>>
{
    public EditSampleParentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSampleParentCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SampleParentState>> Handle(EditSampleParentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSampleParentCommandValidator : AbstractValidator<EditSampleParentCommand>
{
    readonly ApplicationContext _context;

    public EditSampleParentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SampleParentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SampleParent with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<SampleParentState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("SampleParent with name {PropertyValue} already exists");
	
    }
}
