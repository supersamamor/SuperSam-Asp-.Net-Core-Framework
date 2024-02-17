using AutoMapper;
using ProjectNamePlaceHolder.Application.Features.DocumentTypes.Commands.AddEdit;
using ProjectNamePlaceHolder.Application.Features.DocumentTypes.Queries.GetAll;
using ProjectNamePlaceHolder.Application.Features.DocumentTypes.Queries.GetById;
using ProjectNamePlaceHolder.Domain.Entities.Misc;

namespace ProjectNamePlaceHolder.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}