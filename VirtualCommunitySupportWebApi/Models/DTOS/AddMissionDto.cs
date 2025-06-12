namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class AddMissionDto
    {
        public int? id { get; set; }
        public string missionTitle { get; set; }
        public string missionDescription { get; set; }
        public int countryId { get; set; }
        public int cityId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int missionThemeId { get; set; }
        public int missionSkillId { get; set; } 
        public string missionImages { get; set; }  // comma-separated image paths
        public int totalSheets { get; set; }
    }
}
