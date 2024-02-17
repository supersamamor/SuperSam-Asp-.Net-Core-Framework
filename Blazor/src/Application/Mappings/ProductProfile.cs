using AutoMapper;
using ProjectNamePlaceHolder.Application.Features.Products.Commands.AddEdit;
using ProjectNamePlaceHolder.Domain.Entities.Catalog;

namespace ProjectNamePlaceHolder.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}