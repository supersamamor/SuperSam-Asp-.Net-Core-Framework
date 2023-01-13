using AutoMapper;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Application.Features.LocationApi.Approval.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Barangay.Commands;
using CTI.LocationApi.Application.Features.LocationApi.City.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Location.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Province.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Region.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Country.Commands;


namespace CTI.LocationApi.Web.Areas.LocationApi.Mapping;

public class LocationApiProfile : Profile
{
    public LocationApiProfile()
    {
        CreateMap<BarangayViewModel, AddBarangayCommand>();
		CreateMap<BarangayViewModel, EditBarangayCommand>();
		CreateMap<BarangayState, BarangayViewModel>().ForPath(e => e.ForeignKeyCity, o => o.MapFrom(s => s.City!.Code));
		CreateMap<BarangayViewModel, BarangayState>();
		CreateMap<CityViewModel, AddCityCommand>();
		CreateMap<CityViewModel, EditCityCommand>();
		CreateMap<CityState, CityViewModel>().ForPath(e => e.ForeignKeyProvince, o => o.MapFrom(s => s.Province!.Code));
		CreateMap<CityViewModel, CityState>();
		CreateMap<LocationViewModel, AddLocationCommand>();
		CreateMap<LocationViewModel, EditLocationCommand>();
		CreateMap<LocationState, LocationViewModel>().ReverseMap();
		CreateMap<ProvinceViewModel, AddProvinceCommand>();
		CreateMap<ProvinceViewModel, EditProvinceCommand>();
		CreateMap<ProvinceState, ProvinceViewModel>().ForPath(e => e.ForeignKeyRegion, o => o.MapFrom(s => s.Region!.Code));
		CreateMap<ProvinceViewModel, ProvinceState>();
		CreateMap<RegionViewModel, AddRegionCommand>();
		CreateMap<RegionViewModel, EditRegionCommand>();
		CreateMap<RegionState, RegionViewModel>().ForPath(e => e.ForeignKeyCountry, o => o.MapFrom(s => s.Country!.Name));
		CreateMap<RegionViewModel, RegionState>();
		CreateMap<CountryViewModel, AddCountryCommand>();
		CreateMap<CountryViewModel, EditCountryCommand>();
		CreateMap<CountryState, CountryViewModel>().ReverseMap();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
