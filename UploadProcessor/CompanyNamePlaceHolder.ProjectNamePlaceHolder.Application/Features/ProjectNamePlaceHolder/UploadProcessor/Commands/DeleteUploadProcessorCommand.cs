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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Commands;

public record DeleteUploadProcessorCommand : BaseCommand, IRequest<Validation<Error, UploadProcessorState>>;

public class DeleteUploadProcessorCommandHandler : BaseCommandHandler<ApplicationContext, UploadProcessorState, DeleteUploadProcessorCommand>, IRequestHandler<DeleteUploadProcessorCommand, Validation<Error, UploadProcessorState>>
{
    public DeleteUploadProcessorCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUploadProcessorCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UploadProcessorState>> Handle(DeleteUploadProcessorCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUploadProcessorCommandValidator : AbstractValidator<DeleteUploadProcessorCommand>
{
    readonly ApplicationContext _context;

    public DeleteUploadProcessorCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UploadProcessorState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UploadProcessor with id {PropertyValue} does not exists");
    }
}
