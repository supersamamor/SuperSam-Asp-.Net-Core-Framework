using ProjectNamePlaceHolder.Application.Features.Documents.Commands.AddEdit;
using ProjectNamePlaceHolder.Application.Features.Documents.Queries.GetAll;
using ProjectNamePlaceHolder.Application.Requests.Documents;
using ProjectNamePlaceHolder.Shared.Wrapper;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Application.Features.Documents.Queries.GetById;

namespace ProjectNamePlaceHolder.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}