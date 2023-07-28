using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Report.Queries;
public record GetDropdownValuesQuery(string TableKeyValue, string? Filter = "") : IRequest<IList<Dictionary<string, string?>>>;
public class GetDropdownValuesQueryHandler : IRequestHandler<GetDropdownValuesQuery, IList<Dictionary<string, string?>>>
{
    private readonly IConfiguration _configuration;
    public GetDropdownValuesQueryHandler(IConfiguration configuration)
    {    
        _configuration = configuration;
    }

    public async Task<IList<Dictionary<string, string?>>> Handle(GetDropdownValuesQuery request, CancellationToken cancellationToken = default)
    {
        return await Helpers.ReportDataHelper.ConvertTableKeyValueToDictionary(_configuration.GetConnectionString("ReportContext"), request.TableKeyValue,request.Filter);       
    }
}
