using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Application.DTOs;
using LanguageExt;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Queries;

public record GetEmployeeQuery : BaseQuery, IRequest<PagedListResponse<EmployeeListDto>>;

public class GetEmployeeQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, EmployeeListDto, GetEmployeeQuery>(context), IRequestHandler<GetEmployeeQuery, PagedListResponse<EmployeeListDto>>
{
    public override async Task<PagedListResponse<EmployeeListDto>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken = default)
    {
        var pagedList = await Context.Set<EmployeeState>()
            .AsNoTracking().Select(e => new EmployeeListDto()
            {
                Id = e.Id,
                LastModifiedDate = e.LastModifiedDate,
                FirstName = e.FirstName,
                EmployeeCode = e.EmployeeCode,
                LastName = e.LastName,
                MiddleName = e.MiddleName,
            })
            .ToPagedResponse(request.SearchColumns, request.SearchValue,
                request.SortColumn, request.SortOrder,
                request.PageNumber, request.PageSize,
                cancellationToken);
        foreach (var item in pagedList.Data)
        {
            item.StatusBadge = await Helpers.ApprovalHelper.GetApprovalStatus(Context, item.Id, cancellationToken);
        }
        return pagedList;
    }

}
