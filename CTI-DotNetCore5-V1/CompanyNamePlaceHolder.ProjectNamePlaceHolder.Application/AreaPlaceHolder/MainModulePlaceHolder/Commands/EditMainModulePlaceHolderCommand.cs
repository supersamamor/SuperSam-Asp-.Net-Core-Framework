using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands
{
    public record EditMainModulePlaceHolderCommand(
        string Id,
        string Code,
        string Name,
        string Description,
        string Type,
        string Status) : BaseCommand(Id), IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class EditProjectCommandHandler : IRequestHandler<EditMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;

        public EditProjectCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Id == request.Id, cancellationToken).MatchAsync(
                async project => await UpdateProject(request, project, cancellationToken),
                () => Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>($"Project with id {request.Id} does not exist"));

        async Task<Validation<Error, EditMainModulePlaceHolderCommand>> ValidateRequest(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var validations = new List<Validation<Error, EditMainModulePlaceHolderCommand>>()
            {
                await ValidateName(request, cancellationToken),
                await ValidateCode(request, cancellationToken)
            };
            var errors = validations.Map(v => v.Match(_ => None, errors => Some(errors))) //IEnumerable<Option<Seq<Error>>>
                                    .Bind(e => e) //IEnumerable<Seq<Error>>
                                    .Bind(e => e) //IEnumerable<Error>
                                    .ToSeq(); // Seq<Error>
            return errors.Count == 0
                ? Success<Error, EditMainModulePlaceHolderCommand>(request)
                : Validation<Error, EditMainModulePlaceHolderCommand>.Fail(errors);
        }

        async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> UpdateProject(EditMainModulePlaceHolderCommand request, Core.AreaPlaceHolder.MainModulePlaceHolder project, CancellationToken cancellationToken) =>
            await ValidateRequest(request, cancellationToken).BindT(
                async request =>
                {
                    _mapper.Map(request, project);
                    _context.Update(project);
                    _ = await _context.SaveChangesAsync(cancellationToken);
                    return Success<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(project);
                });

        Func<EditMainModulePlaceHolderCommand, CancellationToken, Task<Validation<Error, EditMainModulePlaceHolderCommand>>> ValidateName =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Name == request.Name && p.Id != request.Id, cancellationToken).Match(
                Some: p => Fail<Error, EditMainModulePlaceHolderCommand>($"Project with name {p.Name} already exists"),
                None: () => request
            );

        Func<EditMainModulePlaceHolderCommand, CancellationToken, Task<Validation<Error, EditMainModulePlaceHolderCommand>>> ValidateCode =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Code == request.Code && p.Id != request.Id, cancellationToken).Match(
                Some: p => Fail<Error, EditMainModulePlaceHolderCommand>($"Project with code {p.Code} already exists"),
                None: () => request
            );
    }
}
