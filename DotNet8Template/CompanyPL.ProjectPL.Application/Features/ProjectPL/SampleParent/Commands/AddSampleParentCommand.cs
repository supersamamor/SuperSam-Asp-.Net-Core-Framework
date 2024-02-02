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

public record AddSampleParentCommand : SampleParentState, IRequest<Validation<Error, SampleParentState>>;

public class AddSampleParentCommandHandler : BaseCommandHandler<ApplicationContext, SampleParentState, AddSampleParentCommand>, IRequestHandler<AddSampleParentCommand, Validation<Error, SampleParentState>>
{
	private readonly IdentityContext _identityContext;
    public AddSampleParentCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSampleParentCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, SampleParentState>> Handle(AddSampleParentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddSampleParentCommandValidator : AbstractValidator<AddSampleParentCommand>
{
    readonly ApplicationContext _context;

    public AddSampleParentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SampleParentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SampleParent with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<SampleParentState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("SampleParent with name {PropertyValue} already exists");
	
    }
}
