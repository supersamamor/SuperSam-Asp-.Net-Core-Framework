using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Commands;

public record AddUploadProcessorCommand : UploadProcessorState, IRequest<Validation<Error, UploadProcessorState>>;

public class AddUploadProcessorCommandHandler : BaseCommandHandler<ApplicationContext, UploadProcessorState, AddUploadProcessorCommand>, IRequestHandler<AddUploadProcessorCommand, Validation<Error, UploadProcessorState>>
{
	private readonly IdentityContext _identityContext;
    public AddUploadProcessorCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUploadProcessorCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UploadProcessorState>> Handle(AddUploadProcessorCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddUploadProcessorCommandValidator : AbstractValidator<AddUploadProcessorCommand>
{
    readonly ApplicationContext _context;

    public AddUploadProcessorCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UploadProcessorState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UploadProcessor with id {PropertyValue} already exists");
        
    }
}
