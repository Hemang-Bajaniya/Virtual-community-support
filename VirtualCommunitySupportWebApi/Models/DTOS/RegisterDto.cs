namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class RegisterDto
    {
        public string emailAddress { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string userType { get; set; }

    }

    public class AdminRegisterDto : RegisterDto
    {
        //public string PhoneNumber { get; set; }
    }

}
