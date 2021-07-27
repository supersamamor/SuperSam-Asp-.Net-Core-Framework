using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities
{
    public record AddOrEditEntityCommand : IRequest<Validation<Error, Entity>>
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }

    public class AddOrEditEntityCommandHandler : IRequestHandler<AddOrEditEntityCommand, Validation<Error, Entity>>
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;

        public AddOrEditEntityCommandHandler(IdentityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Validation<Error, Entity>> Handle(AddOrEditEntityCommand request, CancellationToken cancellationToken) =>
            await Optional(await _context.Entities.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken))
            .MatchAsync(
                Some: async entity => await ValidateName(request, cancellationToken)
                .MapAsync(async valid => await valid.MatchAsync(
                    SuccAsync: async request =>
                    {
                        _mapper.Map(request, entity);
                        _context.Update(entity);
                        await _context.SaveChangesAsync(cancellationToken);
                        return Success<Error, Entity>(entity);
                    },
                    Fail: errors => Validation<Error, Entity>.Fail(errors))),
                None: async () =>
                {
                    var entity = _mapper.Map<Entity>(request);
                    _context.Add(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Success<Error, Entity>(entity);
                });

        async Task<Validation<Error, AddOrEditEntityCommand>> ValidateName(AddOrEditEntityCommand request, CancellationToken cancellationToken) =>
            Optional(await _context.Entities.FirstOrDefaultAsync(m => m.Name == request.Name && m.Id != request.Id, cancellationToken))
                .Match(
                e => Fail<Error, AddOrEditEntityCommand>($"Entity with name {request.Name} already exists."),
                () => request);
    }
}
