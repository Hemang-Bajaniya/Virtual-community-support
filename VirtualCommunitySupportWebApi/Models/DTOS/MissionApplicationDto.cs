namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class MissionApplicationDto
    {
        public int Id { get; set; }

        public int Missionid { get; set; }

        public int Userid { get; set; }

        public DateTime Applieddate { get; set; }

        public bool Status { get; set; }

        public int? Sheet { get; set; }

        public string MissionTitle { get; set; }

        public string MissionTheme { get; set; }

        public string UserName { get; set; }
    }
}
