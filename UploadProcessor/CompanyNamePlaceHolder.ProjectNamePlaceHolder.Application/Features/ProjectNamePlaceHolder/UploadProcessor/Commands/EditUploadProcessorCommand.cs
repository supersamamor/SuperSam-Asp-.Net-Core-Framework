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

public record EditUploadProcessorCommand : UploadProcessorState, IRequest<Validation<Error, UploadProcessorState>>;

public class EditUploadProcessorCommandHandler : BaseCommandHandler<ApplicationContext, UploadProcessorState, EditUploadProcessorCommand>, IRequestHandler<EditUploadProcessorCommand, Validation<Error, UploadProcessorState>>
{
    public EditUploadProcessorCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUploadProcessorCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, UploadProcessorState>> Handle(EditUploadProcessorCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditUploadProcessorCommandValidator : AbstractValidator<EditUploadProcessorCommand>
{
    readonly ApplicationContext _context;

    public EditUploadProcessorCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UploadProcessorState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UploadProcessor with id {PropertyValue} does not exists");
        
    }
}
