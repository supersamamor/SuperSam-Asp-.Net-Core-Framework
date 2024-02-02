using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Application.DTOs;
using LanguageExt;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Queries;

public record GetContactInformationQuery : BaseQuery, IRequest<PagedListResponse<ContactInformationListDto>>;

public class GetContactInformationQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, ContactInformationListDto, GetContactInformationQuery>(context), IRequestHandler<GetContactInformationQuery, PagedListResponse<ContactInformationListDto>>
{
    public override async Task<PagedListResponse<ContactInformationListDto>> Handle(GetContactInformationQuery request, CancellationToken cancellationToken = default)
    {

        return await Context.Set<ContactInformationState>().Include(l => l.Employee)
            .AsNoTracking().Select(e => new ContactInformationListDto()
            {
                Id = e.Id,
                LastModifiedDate = e.LastModifiedDate,
                EmployeeId = e.Employee == null ? "" : e.Employee.EmployeeCode,
                ContactDetails = e.ContactDetails,
            })
            .ToPagedResponse(request.SearchColumns, request.SearchValue,
                request.SortColumn, request.SortOrder,
                request.PageNumber, request.PageSize,
                cancellationToken);
    }
}
