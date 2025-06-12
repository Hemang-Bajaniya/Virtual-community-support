namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class CountryDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class CityDto : CountryDto
    { }
}
