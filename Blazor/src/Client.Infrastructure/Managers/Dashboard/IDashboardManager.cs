using ProjectNamePlaceHolder.Shared.Wrapper;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Application.Features.Dashboards.Queries.GetData;

namespace ProjectNamePlaceHolder.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}