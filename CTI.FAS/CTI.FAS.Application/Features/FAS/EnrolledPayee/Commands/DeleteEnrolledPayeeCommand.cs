using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;

public record DeleteEnrolledPayeeCommand : BaseCommand, IRequest<Validation<Error, EnrolledPayeeState>>;

public class DeleteEnrolledPayeeCommandHandler : BaseCommandHandler<ApplicationContext, EnrolledPayeeState, DeleteEnrolledPayeeCommand>, IRequestHandler<DeleteEnrolledPayeeCommand, Validation<Error, EnrolledPayeeState>>
{
    public DeleteEnrolledPayeeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteEnrolledPayeeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, EnrolledPayeeState>> Handle(DeleteEnrolledPayeeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteEnrolledPayeeCommandValidator : AbstractValidator<DeleteEnrolledPayeeCommand>
{
    readonly ApplicationContext _context;

    public DeleteEnrolledPayeeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EnrolledPayeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EnrolledPayee with id {PropertyValue} does not exists");
    }
}
