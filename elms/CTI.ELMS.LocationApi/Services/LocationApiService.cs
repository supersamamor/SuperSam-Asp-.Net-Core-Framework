using CTI.ELMS.LocationApi.Exceptions;
using CTI.ELMS.LocationApi.Models;
using CTI.ELMS.LocationApi.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CTI.ELMS.LocationApi.Services
{
    public class LocationApiService
    {
        private readonly LocationApiSettings _locationApiSettings;
        private readonly HttpClient _client;
        public LocationApiService(IOptions<LocationApiSettings> locationApiSettings, HttpClient client)
        {
            _locationApiSettings = locationApiSettings.Value;
            _client = client;
        }
        public async Task<IList<Country>?> GetCountryList(string? searchString = null)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Countries?SearchString=" + searchString + "&pageSize=500");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<PagedListApiResponse<Country>>(result)?.Data;
        }

        public async Task<IList<Address>?> GetRegionList(string? searchString = null)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Regions?AlternativeName=" + searchString + "&pageSize=500");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<PagedListApiResponse<Address>>(result)?.Data;
        }
        public async Task<Address?> GetRegion(string code)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Regions/" + code);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<Address>(result);
        }

        public async Task<IList<Address>?> GetProvinceListViaRegionCode(string regionCode, string? searchString = null)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Regions/" + regionCode + "/Provinces?AlternativeName=" + searchString + "&pageSize=500");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<PagedListApiResponse<Address>>(result)?.Data;
        }

        public async Task<IList<Address>?> GetProvinceList(string? searchString = null)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Provinces?AlternativeName=" + searchString + "&pageSize=500");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<PagedListApiResponse<Address>>(result)?.Data;
        }
        public async Task<Address?> GetProvince(string code)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Provinces/" + code);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<Address>(result);
        }
        public async Task<IList<Address>?> GetCityList(string provinceCode, string? searchString = null)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Provinces/" + provinceCode + "/Cities?AlternativeName=" + searchString + "&pageSize=500");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<PagedListApiResponse<Address>>(result)?.Data;
        }
        public async Task<Address?> GetCity(string code)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Cities/" + code);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<Address>(result);
        }
        public async Task<IList<Address>?> GetBarangayList(string cityCode, string? searchString = null)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Cities/" + cityCode + "/Barangays?AlternativeName=" + searchString + "&pageSize=500");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<PagedListApiResponse<Address>>(result)?.Data;
        }

        public async Task<Address?> GetBarangay(string code)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + "/api/v1/Barangays/" + code);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<Address>(result);
        }
        public async Task<IList<Address>?> GetCityListByProvinceName(string provinceName)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + $"/api/v1/Provinces/CitiesByProvinceName?ProvinceName={provinceName}");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<IList<Address>>(result);
        }
        public async Task<IList<Address>?> GetBarangayListByProvinceNameAndCityName(string provinceName, string cityName)
        {
            var response = await _client.GetAsync(_locationApiSettings.LocationApiUrl + $"/api/v1/Cities/BarangaysByProvinceNameAndCityName?ProvinceName={provinceName}&CityName={cityName}");
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            return JsonConvert.DeserializeObject<IList<Address>>(result);
        }
    }
}
