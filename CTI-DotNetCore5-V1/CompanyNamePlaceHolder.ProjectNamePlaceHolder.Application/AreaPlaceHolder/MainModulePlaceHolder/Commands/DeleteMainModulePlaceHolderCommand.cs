using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands
{
    public record DeleteMainModulePlaceHolderCommand(string Id) : BaseCommand(Id), IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        private readonly ApplicationContext _context;

        public DeleteProjectCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(DeleteMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.GetSingle((Core.AreaPlaceHolder.MainModulePlaceHolder p) => p.Id == request.Id, cancellationToken);
            return await project.MatchAsync(
                Some: async p =>
                 {
                     _context.Projects.Remove(p);
                     await _context.SaveChangesAsync(cancellationToken);
                     return Success<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(p);
                 },
                None: () =>
                {
                    return Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>($"Project not found");
                });
        }
    }
}
