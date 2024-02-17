using AutoMapper;
using ProjectNamePlaceHolder.Application.Features.Brands.Commands.AddEdit;
using ProjectNamePlaceHolder.Application.Features.Brands.Queries.GetAll;
using ProjectNamePlaceHolder.Application.Features.Brands.Queries.GetById;
using ProjectNamePlaceHolder.Domain.Entities.Catalog;

namespace ProjectNamePlaceHolder.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}