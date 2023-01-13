using AutoMapper;
using CTI.LocationApi.API.Controllers.v1;
using CTI.LocationApi.Application.Features.LocationApi.Barangay.Commands;
using CTI.LocationApi.Application.Features.LocationApi.City.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Location.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Province.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Region.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Country.Commands;


namespace CTI.LocationApi.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BarangayViewModel, AddBarangayCommand>();
		CreateMap <BarangayViewModel, EditBarangayCommand>();
		CreateMap<CityViewModel, AddCityCommand>();
		CreateMap <CityViewModel, EditCityCommand>();
		CreateMap<LocationViewModel, AddLocationCommand>();
		CreateMap <LocationViewModel, EditLocationCommand>();
		CreateMap<ProvinceViewModel, AddProvinceCommand>();
		CreateMap <ProvinceViewModel, EditProvinceCommand>();
		CreateMap<RegionViewModel, AddRegionCommand>();
		CreateMap <RegionViewModel, EditRegionCommand>();
		CreateMap<CountryViewModel, AddCountryCommand>();
		CreateMap <CountryViewModel, EditCountryCommand>();
		
    }
}
