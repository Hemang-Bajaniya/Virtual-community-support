namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class ChnagePassDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
