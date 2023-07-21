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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;

public record EditTestCommand : TestState, IRequest<Validation<Error, TestState>>;

public class EditTestCommandHandler : BaseCommandHandler<ApplicationContext, TestState, EditTestCommand>, IRequestHandler<EditTestCommand, Validation<Error, TestState>>
{
    public EditTestCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTestCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TestState>> Handle(EditTestCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTestCommandValidator : AbstractValidator<EditTestCommand>
{
    readonly ApplicationContext _context;

    public EditTestCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TestState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Test with id {PropertyValue} does not exists");
        
    }
}
