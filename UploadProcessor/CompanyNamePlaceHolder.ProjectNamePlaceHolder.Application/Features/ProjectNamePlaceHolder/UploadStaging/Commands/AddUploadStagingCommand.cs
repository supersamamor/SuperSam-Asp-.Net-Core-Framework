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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Commands;

public record AddUploadStagingCommand : UploadStagingState, IRequest<Validation<Error, UploadStagingState>>;

public class AddUploadStagingCommandHandler : BaseCommandHandler<ApplicationContext, UploadStagingState, AddUploadStagingCommand>, IRequestHandler<AddUploadStagingCommand, Validation<Error, UploadStagingState>>
{
	private readonly IdentityContext _identityContext;
    public AddUploadStagingCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUploadStagingCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UploadStagingState>> Handle(AddUploadStagingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddUploadStagingCommandValidator : AbstractValidator<AddUploadStagingCommand>
{
    readonly ApplicationContext _context;

    public AddUploadStagingCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UploadStagingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UploadStaging with id {PropertyValue} already exists");
        
    }
}
