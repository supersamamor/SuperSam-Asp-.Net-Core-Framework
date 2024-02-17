using ProjectNamePlaceHolder.Application.Interfaces.Repositories;
using ProjectNamePlaceHolder.Domain.Entities.Catalog;

namespace ProjectNamePlaceHolder.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}