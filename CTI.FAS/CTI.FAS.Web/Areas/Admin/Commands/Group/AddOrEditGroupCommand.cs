using AutoMapper;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using CTI.FAS.Core.Identity;

namespace CTI.FAS.Web.Areas.Admin.Commands.Group;

public record AddOrEditGroupCommand : IRequest<Validation<Error, Core.Identity.Group>>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? ContactDetails { get; set; }
}

public class AddOrEditGroupCommandHandler : IRequestHandler<AddOrEditGroupCommand, Validation<Error, Core.Identity.Group>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public AddOrEditGroupCommandHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Validation<Error, Core.Identity.Group>> Handle(AddOrEditGroupCommand request, CancellationToken cancellationToken) =>
        await Optional(await _context.Group.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken))
        .MatchAsync(
            Some: async group => await ValidateName(request, cancellationToken)
            .MapAsync(async valid => await valid.MatchAsync(
                SuccAsync: async request =>
                {
                    _mapper.Map(request, group);
                    _context.Update(group!);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Success<Error, Core.Identity.Group>(group!);
                },
                Fail: errors => Validation<Error, Core.Identity.Group>.Fail(errors))),
            None: async () =>
            {
                var group = _mapper.Map<Core.Identity.Group>(request);
                _context.Add(group);
                await _context.SaveChangesAsync(cancellationToken);
                return Success<Error, Core.Identity.Group>(group);
            });

    async Task<Validation<Error, AddOrEditGroupCommand>> ValidateName(AddOrEditGroupCommand request, CancellationToken cancellationToken) =>
        Optional(await _context.Group.FirstOrDefaultAsync(m => m.Name == request.Name && m.Id != request.Id, cancellationToken))
            .Match(
            e => Fail<Error, AddOrEditGroupCommand>($"Group with name {request.Name} already exists."),
            () => request);
}
