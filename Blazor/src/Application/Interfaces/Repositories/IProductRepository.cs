using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsBrandUsed(int brandId);
    }
}