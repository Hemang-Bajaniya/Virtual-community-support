namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class UserInfo
    {
        public int id { get; set; }
        public string emailAddress { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string userType { get; set; }
    }
}
