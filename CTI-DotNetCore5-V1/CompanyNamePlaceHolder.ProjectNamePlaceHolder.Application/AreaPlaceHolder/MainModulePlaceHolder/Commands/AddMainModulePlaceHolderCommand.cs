using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using LanguageExt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands
{
    public record AddMainModulePlaceHolderCommand(
        string Id,
        string Code,
        string Name,
        string Description,
        string Type,
        string Status) : BaseCommand(Id), IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class AddProjectCommandHandler : IRequestHandler<AddMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public AddProjectCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
            await ValidateRequest(request, cancellationToken).BindT(async request =>
            {
                var project = _mapper.Map<Core.AreaPlaceHolder.MainModulePlaceHolder>(request);
                _context.Add(project);
                _ = await _context.SaveChangesAsync(cancellationToken);
                return Success<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(project);
            });

        async Task<Validation<Error, AddMainModulePlaceHolderCommand>> ValidateRequest(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var validations = new List<Validation<Error, AddMainModulePlaceHolderCommand>>()
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
                ? Success<Error, AddMainModulePlaceHolderCommand>(request)
                : Validation<Error, AddMainModulePlaceHolderCommand>.Fail(errors);
        }

        Func<AddMainModulePlaceHolderCommand, CancellationToken, Task<Validation<Error, AddMainModulePlaceHolderCommand>>> ValidateId =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Id == request.Id, cancellationToken).Match(
                Some: p => Fail<Error, AddMainModulePlaceHolderCommand>($"Project with id {p.Id} already exists"),
                None: () => request
            );

        Func<AddMainModulePlaceHolderCommand, CancellationToken, Task<Validation<Error, AddMainModulePlaceHolderCommand>>> ValidateName =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Name == request.Name, cancellationToken).Match(
                Some: p => Fail<Error, AddMainModulePlaceHolderCommand>($"Project with name {p.Name} already exists"),
                None: () => request
            );

        Func<AddMainModulePlaceHolderCommand, CancellationToken, Task<Validation<Error, AddMainModulePlaceHolderCommand>>> ValidateCode =>
            async (request, cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Code == request.Code, cancellationToken).Match(
                Some: p => Fail<Error, AddMainModulePlaceHolderCommand>($"Project with code {p.Code} already exists"),
                None: () => request
            );
    }
}
