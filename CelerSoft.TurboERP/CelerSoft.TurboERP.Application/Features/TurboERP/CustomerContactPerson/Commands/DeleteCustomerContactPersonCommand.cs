using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Commands;

public record DeleteCustomerContactPersonCommand : BaseCommand, IRequest<Validation<Error, CustomerContactPersonState>>;

public class DeleteCustomerContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, CustomerContactPersonState, DeleteCustomerContactPersonCommand>, IRequestHandler<DeleteCustomerContactPersonCommand, Validation<Error, CustomerContactPersonState>>
{
    public DeleteCustomerContactPersonCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCustomerContactPersonCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CustomerContactPersonState>> Handle(DeleteCustomerContactPersonCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCustomerContactPersonCommandValidator : AbstractValidator<DeleteCustomerContactPersonCommand>
{
    readonly ApplicationContext _context;

    public DeleteCustomerContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CustomerContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CustomerContactPerson with id {PropertyValue} does not exists");
    }
}
