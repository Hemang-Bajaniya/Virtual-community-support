namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class UpdateUserDto
    {
        public int id { get; set; }
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string emailAddress { get; set; } = null!;
        public string? phoneNumber { get; set; }
        public string userType { get; set; }
    }
}
