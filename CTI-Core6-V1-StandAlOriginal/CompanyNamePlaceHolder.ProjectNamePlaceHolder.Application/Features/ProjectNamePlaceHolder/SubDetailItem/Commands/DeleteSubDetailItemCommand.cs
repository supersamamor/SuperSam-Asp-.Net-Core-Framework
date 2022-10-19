using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Commands;

public record DeleteSubDetailItemCommand : BaseCommand, IRequest<Validation<Error, SubDetailItemState>>;

public class DeleteSubDetailItemCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailItemState, DeleteSubDetailItemCommand>, IRequestHandler<DeleteSubDetailItemCommand, Validation<Error, SubDetailItemState>>
{
    public DeleteSubDetailItemCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSubDetailItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SubDetailItemState>> Handle(DeleteSubDetailItemCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSubDetailItemCommandValidator : AbstractValidator<DeleteSubDetailItemCommand>
{
    readonly ApplicationContext _context;

    public DeleteSubDetailItemCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailItem with id {PropertyValue} does not exists");
    }
}
