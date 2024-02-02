using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Application.DTOs;
using LanguageExt;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Queries;

public record GetHealthDeclarationQuery : BaseQuery, IRequest<PagedListResponse<HealthDeclarationListDto>>;

public class GetHealthDeclarationQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, HealthDeclarationListDto, GetHealthDeclarationQuery>(context), IRequestHandler<GetHealthDeclarationQuery, PagedListResponse<HealthDeclarationListDto>>
{
    public override async Task<PagedListResponse<HealthDeclarationListDto>> Handle(GetHealthDeclarationQuery request, CancellationToken cancellationToken = default)
    {

        return await Context.Set<HealthDeclarationState>().Include(l => l.Employee)
            .AsNoTracking().Select(e => new HealthDeclarationListDto()
            {
                Id = e.Id,
                LastModifiedDate = e.LastModifiedDate,
                EmployeeId = e.Employee == null ? "" : e.Employee!.EmployeeCode,
                IsVaccinated = e.IsVaccinated == true ? "Yes" : "No",
                Vaccine = e.Vaccine,
            })
            .ToPagedResponse(request.SearchColumns, request.SearchValue,
                request.SortColumn, request.SortOrder,
                request.PageNumber, request.PageSize,
                cancellationToken);
    }
}
