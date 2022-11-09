namespace CTI.ELMS.LocationApi.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string BarangayCode { get; set; } = "";
        public string Barangay { get; set; } = "";
        public string CityCode { get; set; } = "";
        public string City { get; set; } = "";
        public string ProvinceCode { get; set; } = "";
        public string Province { get; set; } = "";
        public string RegionCode { get; set; } = "";
        public string Region { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
