using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.MainModulePlaceHolder;
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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.MainModulePlaceHolder.Projects.Commands
{
    public record DeleteProjectCommand(string Id) : BaseCommand(Id), IRequest<Validation<Error, Project>>;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Validation<Error, Project>>
    {
        private readonly ApplicationContext _context;

        public DeleteProjectCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Validation<Error, Project>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.GetSingle<Project>(p => p.Id == request.Id, cancellationToken);
            return await project.MatchAsync(
                Some: async p =>
                 {
                     _context.Projects.Remove(p);
                     await _context.SaveChangesAsync(cancellationToken);
                     return Success<Error, Project>(p);
                 },
                None: () =>
                {
                    return Fail<Error, Project>($"Project not found");
                });
        }
    }
}
