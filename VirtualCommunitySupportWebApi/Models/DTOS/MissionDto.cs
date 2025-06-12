namespace VirtualCommunitySupportWebApi.Models.DTOS
{
    public class MissionDto
    {
        public int Id { get; set; }
        public string MissionTitle { get; set; } = null!;
        public string? MissionDescription { get; set; }
        public string? MissionOrganisationName { get; set; }
        public string? MissionOrganisationDetail { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? MissionDeadLineStatus { get; set; }
        public string? MissionType { get; set; }
        public int? TotalSheets { get; set; }
        public DateOnly? RegistrationDeadline { get; set; }
        public string? MissionImages { get; set; }
        public string? MissionDocuments { get; set; }
        public string? MissionAvilability { get; set; }
        public string? MissionVideoUrl { get; set; }

        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public string? SkillName { get; set; }
        public string? ThemeName { get; set; }

        public string? MissionStatus { get; set; }
        public string? MissionApplyStatus { get; set; }
        public string? MissionApprovedStatus { get; set; }

    }

}
