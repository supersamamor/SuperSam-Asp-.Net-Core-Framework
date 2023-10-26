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

public record EditUploadStagingCommand : UploadStagingState, IRequest<Validation<Error, UploadStagingState>>;

public class EditUploadStagingCommandHandler : BaseCommandHandler<ApplicationContext, UploadStagingState, EditUploadStagingCommand>, IRequestHandler<EditUploadStagingCommand, Validation<Error, UploadStagingState>>
{
    public EditUploadStagingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUploadStagingCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, UploadStagingState>> Handle(EditUploadStagingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditUploadStagingCommandValidator : AbstractValidator<EditUploadStagingCommand>
{
    readonly ApplicationContext _context;

    public EditUploadStagingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UploadStagingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UploadStaging with id {PropertyValue} does not exists");
        
    }
}
