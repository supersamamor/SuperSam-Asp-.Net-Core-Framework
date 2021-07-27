using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.Projects.Commands
{
    public record EditProjectCommand(
        string Id,
        string Code,
        string Name,
        string Description,
        string Type,
        string Status) : BaseCommand(Id), IRequest<Validation<Error, Project>>;

    public class EditProjectCommandHandler : IRequestHandler<EditProjectCommand, Validation<Error, Project>>
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;

        public EditProjectCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Validation<Error, Project>> Handle(EditProjectCommand request, CancellationToken cancellationToken) =>
            await _context.GetSingle<Project>(p => p.Id == request.Id, cancellationToken).MatchAsync(
                async project => await UpdateProject(request, project, cancellationToken),
                () => Fail<Error, Project>($"Project with id {request.Id} does not exist"));

        async Task<Validation<Error, EditProjectCommand>> ValidateRequest(EditProjectCommand request, CancellationToken cancellationToken)
        {
            var validations = new List<Validation<Error, EditProjectCommand>>()
            {
                await ValidateName(request, cancellationToken),
                await ValidateCode(request, cancellationToken)
            };
            var errors = validations.Map(v => v.Match(_ => None, errors => Some(errors))) //IEnumerable<Option<Seq<Error>>>
                                    .Bind(e => e) //IEnumerable<Seq<Error>>
                                    .Bind(e => e) //IEnumerable<Error>
                                    .ToSeq(); // Seq<Error>
            return errors.Count == 0
                ? Success<Error, EditProjectCommand>(request)
                : Validation<Error, EditProjectCommand>.Fail(errors);
        }

        async Task<Validation<Error, Project>> UpdateProject(EditProjectCommand request, Project project, CancellationToken cancellationToken) =>
            await ValidateRequest(request, cancellationToken).BindT(
                async request =>
                {
                    _mapper.Map(request, project);
                    _context.Update(project);
                    _ = await _context.SaveChangesAsync(cancellationToken);
                    return Success<Error, Project>(project);
                });

        Func<EditProjectCommand, CancellationToken, Task<Validation<Error, EditProjectCommand>>> ValidateName =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Project>(p => p.Name == request.Name && p.Id != request.Id, cancellationToken).Match(
                Some: p => Fail<Error, EditProjectCommand>($"Project with name {p.Name} already exists"),
                None: () => request
            );

        Func<EditProjectCommand, CancellationToken, Task<Validation<Error, EditProjectCommand>>> ValidateCode =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Project>(p => p.Code == request.Code && p.Id != request.Id, cancellationToken).Match(
                Some: p => Fail<Error, EditProjectCommand>($"Project with code {p.Code} already exists"),
                None: () => request
            );
    }
}
