using AutoMapper;
using ProjectNamePlaceHolder.Application.Features.Documents.Commands.AddEdit;
using ProjectNamePlaceHolder.Application.Features.Documents.Queries.GetById;
using ProjectNamePlaceHolder.Domain.Entities.Misc;

namespace ProjectNamePlaceHolder.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}