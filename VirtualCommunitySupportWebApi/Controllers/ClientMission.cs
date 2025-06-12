using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualCommunitySupportWebApi.Data;
using VirtualCommunitySupportWebApi.Models;
using VirtualCommunitySupportWebApi.Models.DTOS;
using VirtualCommunitySupportWebApi.Util;

namespace VirtualCommunitySupportWebApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class ClientMission : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientMission(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize]
        [HttpGet("ClientSideMissionList/{userId}")]
        public async Task<IActionResult> ClientsideMissionList(int userId)
        {
            var missions = await _context.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
                .Include(m => m.Missionskill)
                .Include(m => m.Missiontheme)
                .Select(m => new MissionDto
                {
                    Id = m.Id,
                    MissionTitle = m.Missiontitle,
                    MissionDescription = m.Missiondescription,
                    MissionOrganisationName = m.Missionorganisationname,
                    MissionOrganisationDetail = m.Missionorganisationdetail,
                    CountryId = m.Countryid,
                    CityId = m.Cityid,
                    StartDate = m.Startdate,
                    EndDate = m.Enddate,
                    MissionType = m.Missiontype,
                    TotalSheets = m.Totalsheets,
                    RegistrationDeadline = m.Registrationdeadline,
                    MissionImages = m.Missionimages,
                    MissionDocuments = m.Missiondocuments,
                    MissionAvilability = m.Missionavilability,
                    MissionVideoUrl = m.Missionvideourl,
                    CityName = m.City != null ? m.City.Cityname : null,
                    CountryName = m.Country != null ? m.Country.Countryname : null,
                    SkillName = m.Missionskill != null ? m.Missionskill.Skillname : null,
                    ThemeName = m.Missiontheme != null ? m.Missiontheme.Themename : null,

                    MissionApplyStatus = m.Missionapplications.Any(m => m.Userid == userId) ? "Applied" : "Apply",
                    MissionApprovedStatus = m.Missionapplications.Any(m => m.Userid == userId && m.Status) ? "Approved" : "Applied",
                    MissionStatus = m.Registrationdeadline.HasValue && m.Registrationdeadline.Value < DateOnly.FromDateTime(DateTime.Now) ? "Closed" : "Available",
                })
                .ToListAsync();


            return Ok(new ApiResult<IList<MissionDto>>
            {
                result = 1,
                data = new ApiResponse<IList<MissionDto>>
                {
                    Data = missions,
                    Message = "Client side missionlist fetched"
                }
            });
        }

        [Authorize]
        [HttpPost("ApplyMission")]
        public async Task<IActionResult> ApplyMission(MissionAppDto dto)
        {
            var mission = await _context.Missions.FindAsync(dto.MissionId);

            if (mission == null)
                return NotFound("No such mission for app available");

            var applied = _context.Missionapplications.Any(m => m.Userid == dto.UserId && m.Missionid == dto.MissionId);

            if (applied)
                return BadRequest("You have already applied for this mission");

            Missionapplication missionapplication = new Missionapplication
            {
                Applieddate = dto.AppliededDate,
                Missionid = dto.MissionId,
                Userid = dto.UserId,
                Sheet = dto.sheet,
                Status = dto.Status,
            };

            await _context.Missionapplications.AddAsync(missionapplication);
            await _context.SaveChangesAsync();

            return Ok(new { result = 1, data = "Applied to mission successfully" });
        }

        [Authorize]
        [HttpPost("MissionClientList")]
        public IActionResult GetMissionClientList([FromBody] MissionClientListRequest request)
        {
            var missions = _context.Missions
                .Include(m => m.Country)
                .Include(m => m.City)
                .Include(m => m.Missiontheme)
                .Include(m => m.Missionskill)
                .AsQueryable();

            switch (request.SortestValue)
            {
                case "Newest":
                    missions = missions.OrderByDescending(m => m.Startdate);
                    break;
                case "Oldest":
                    missions = missions.OrderBy(m => m.Startdate);
                    break;
                case "Lowest available seats":
                    missions = missions.OrderBy(m => m.Totalsheets);
                    break;
                case "Highest available seats":
                    missions = missions.OrderByDescending(m => m.Totalsheets);
                    break;
                case "Registration deadline":
                    missions = missions.OrderBy(m => m.Registrationdeadline);
                    break;
                default:
                    missions = missions.OrderByDescending(m => m.Id);
                    break;
            }

            var result = missions.Select(m => new MissionDto
            {
                Id = m.Id,
                MissionTitle = m.Missiontitle,
                MissionDescription = m.Missiondescription,
                MissionOrganisationName = m.Missionorganisationname,
                MissionOrganisationDetail = m.Missionorganisationdetail,
                CountryId = m.Countryid,
                CityId = m.Cityid,
                StartDate = m.Startdate,
                EndDate = m.Enddate,
                MissionType = m.Missiontype,
                TotalSheets = m.Totalsheets,
                RegistrationDeadline = m.Registrationdeadline,
                MissionImages = m.Missionimages,
                MissionDocuments = m.Missiondocuments,
                MissionAvilability = m.Missionavilability,
                MissionVideoUrl = m.Missionvideourl,
                CityName = m.City != null ? m.City.Cityname : null,
                CountryName = m.Country != null ? m.Country.Countryname : null,
                SkillName = m.Missionskill != null ? m.Missionskill.Skillname : null,
                ThemeName = m.Missiontheme != null ? m.Missiontheme.Themename : null,

                MissionApplyStatus = m.Missionapplications.Any(m => m.Userid == request.UserId) ? "Applied" : "Apply",
                MissionApprovedStatus = m.Missionapplications.Any(m => m.Userid == request.UserId && m.Status) ? "Approved" : "Applied",
                MissionStatus = m.Registrationdeadline.HasValue && m.Registrationdeadline.Value < DateOnly.FromDateTime(DateTime.Now) ? "Closed" : "Available",
            }).ToList();

            return Ok(new { result = 1, data = result });
        }



    }
}
