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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Commands;

public record DeleteUploadStagingCommand : BaseCommand, IRequest<Validation<Error, UploadStagingState>>;

public class DeleteUploadStagingCommandHandler : BaseCommandHandler<ApplicationContext, UploadStagingState, DeleteUploadStagingCommand>, IRequestHandler<DeleteUploadStagingCommand, Validation<Error, UploadStagingState>>
{
    public DeleteUploadStagingCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUploadStagingCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UploadStagingState>> Handle(DeleteUploadStagingCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUploadStagingCommandValidator : AbstractValidator<DeleteUploadStagingCommand>
{
    readonly ApplicationContext _context;

    public DeleteUploadStagingCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UploadStagingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UploadStaging with id {PropertyValue} does not exists");
    }
}
