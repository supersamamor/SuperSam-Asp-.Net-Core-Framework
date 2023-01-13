using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.LocationApi.Application.Features.LocationApi.Approval.Commands;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Application.Features.LocationApi.Barangay.Commands;
using CTI.LocationApi.Application.Features.LocationApi.City.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Location.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Province.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Region.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Country.Commands;



namespace CTI.LocationApi.Application.Features.LocationApi;

public class LocationApiProfile : Profile
{
    public LocationApiProfile()
    {
        CreateMap<AddBarangayCommand, BarangayState>();
		CreateMap <EditBarangayCommand, BarangayState>().IgnoreBaseEntityProperties();
		CreateMap<AddCityCommand, CityState>();
		CreateMap <EditCityCommand, CityState>().IgnoreBaseEntityProperties();
		CreateMap<AddLocationCommand, LocationState>();
		CreateMap <EditLocationCommand, LocationState>().IgnoreBaseEntityProperties();
		CreateMap<AddProvinceCommand, ProvinceState>();
		CreateMap <EditProvinceCommand, ProvinceState>().IgnoreBaseEntityProperties();
		CreateMap<AddRegionCommand, RegionState>();
		CreateMap <EditRegionCommand, RegionState>().IgnoreBaseEntityProperties();
		CreateMap<AddCountryCommand, CountryState>();
		CreateMap <EditCountryCommand, CountryState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
