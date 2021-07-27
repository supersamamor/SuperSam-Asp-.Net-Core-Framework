using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using LanguageExt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Inventory.Projects.Commands
{
    public record AddProjectCommand(
        string Id,
        string Code,
        string Name,
        string Description,
        string Type,
        string Status) : BaseCommand(Id), IRequest<Validation<Error, Project>>;

    public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, Validation<Error, Project>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public AddProjectCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Validation<Error, Project>> Handle(AddProjectCommand request, CancellationToken cancellationToken) =>
            await ValidateRequest(request, cancellationToken).BindT(async request =>
            {
                var project = _mapper.Map<Project>(request);
                _context.Add(project);
                _ = await _context.SaveChangesAsync(cancellationToken);
                return Success<Error, Project>(project);
            });

        async Task<Validation<Error, AddProjectCommand>> ValidateRequest(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var validations = new List<Validation<Error, AddProjectCommand>>()
            {
                await ValidateId(request, cancellationToken),
                await ValidateName(request, cancellationToken),
                await ValidateCode(request, cancellationToken)
            };
            var errors = validations.Map(v => v.Match(_ => None, errors => Some(errors))) //IEnumerable<Option<Seq<Error>>>
                                    .Bind(e => e) //IEnumerable<Seq<Error>>
                                    .Bind(e => e) //IEnumerable<Error>
                                    .ToSeq(); // Seq<Error>
            return errors.Count == 0
                ? Success<Error, AddProjectCommand>(request)
                : Validation<Error, AddProjectCommand>.Fail(errors);
        }

        Func<AddProjectCommand, CancellationToken, Task<Validation<Error, AddProjectCommand>>> ValidateId =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Project>(p => p.Id == request.Id, cancellationToken).Match(
                Some: p => Fail<Error, AddProjectCommand>($"Project with id {p.Id} already exists"),
                None: () => request
            );

        Func<AddProjectCommand, CancellationToken, Task<Validation<Error, AddProjectCommand>>> ValidateName =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Project>(p => p.Name == request.Name, cancellationToken).Match(
                Some: p => Fail<Error, AddProjectCommand>($"Project with name {p.Name} already exists"),
                None: () => request
            );

        Func<AddProjectCommand, CancellationToken, Task<Validation<Error, AddProjectCommand>>> ValidateCode =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Project>(p => p.Code == request.Code, cancellationToken).Match(
                Some: p => Fail<Error, AddProjectCommand>($"Project with code {p.Code} already exists"),
                None: () => request
            );
    }
}
