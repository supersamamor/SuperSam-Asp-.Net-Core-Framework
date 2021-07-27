using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Inventory.Projects.Queries
{
    public record GetProjectByIdQuery(string Id) : IRequest<Option<Project>>;

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Option<Project>>
    {
        private readonly ApplicationContext _context;

        public GetProjectByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Option<Project>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken) =>
            await _context.Projects.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
    }
}
