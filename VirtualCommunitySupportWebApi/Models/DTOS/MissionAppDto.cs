namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class MissionAppDto
    {
        public DateTime AppliededDate { get; set; }
        public int sheet { get; set; }
        public bool Status { get; set; }
        public int MissionId { get; set; }
        public int UserId { get; set; }
    }
}
