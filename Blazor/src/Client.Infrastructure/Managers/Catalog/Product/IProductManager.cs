using ProjectNamePlaceHolder.Application.Features.Products.Commands.AddEdit;
using ProjectNamePlaceHolder.Application.Features.Products.Queries.GetAllPaged;
using ProjectNamePlaceHolder.Application.Requests.Catalog;
using ProjectNamePlaceHolder.Shared.Wrapper;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}