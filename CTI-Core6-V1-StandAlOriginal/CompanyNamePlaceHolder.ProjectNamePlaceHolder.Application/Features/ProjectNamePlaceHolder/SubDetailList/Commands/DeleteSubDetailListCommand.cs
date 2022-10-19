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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Commands;

public record DeleteSubDetailListCommand : BaseCommand, IRequest<Validation<Error, SubDetailListState>>;

public class DeleteSubDetailListCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailListState, DeleteSubDetailListCommand>, IRequestHandler<DeleteSubDetailListCommand, Validation<Error, SubDetailListState>>
{
    public DeleteSubDetailListCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSubDetailListCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SubDetailListState>> Handle(DeleteSubDetailListCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSubDetailListCommandValidator : AbstractValidator<DeleteSubDetailListCommand>
{
    readonly ApplicationContext _context;

    public DeleteSubDetailListCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailListState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailList with id {PropertyValue} does not exists");
    }
}
