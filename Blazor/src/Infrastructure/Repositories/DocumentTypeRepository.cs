using ProjectNamePlaceHolder.Application.Interfaces.Repositories;
using ProjectNamePlaceHolder.Domain.Entities.Misc;

namespace ProjectNamePlaceHolder.Infrastructure.Repositories
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly IRepositoryAsync<DocumentType, int> _repository;

        public DocumentTypeRepository(IRepositoryAsync<DocumentType, int> repository)
        {
            _repository = repository;
        }
    }
}