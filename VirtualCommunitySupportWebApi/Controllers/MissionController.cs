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
    public class MissionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MissionController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize]
        [HttpGet("MissionList")]
        public async Task<IActionResult> GetMissionList()
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
                    MissionDeadLineStatus = m.Registrationdeadline.ToString()
                })
                .ToListAsync();

            return Ok(new ApiResult<IList<MissionDto>>
            {
                result = 1,
                data = new ApiResponse<IList<MissionDto>>
                {
                    Data = missions,
                    Message = "Missionlist fetched successfully"
                }
            });
        }

        /// <summary>
        /// add_msn
        /// </summary>
        /// <param name="addMission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("AddMission")]
        public async Task<IActionResult> AddMission(AddMissionDto addMission)
        {
            bool exists = _context.Missions.Any(m => m.Missiontitle.ToLower() == addMission.missionTitle.ToLower());

            if (exists)
                return BadRequest("Mission with same title already avialable");

            Mission mission = new Mission
            {
                Missiontitle = addMission.missionTitle,
                Missiondescription = addMission.missionDescription,
                Countryid = addMission.countryId,
                Cityid = addMission.cityId,
                Startdate = DateOnly.FromDateTime(addMission.startDate),
                Enddate = DateOnly.FromDateTime(addMission.endDate),
                Missionthemeid = addMission.missionThemeId,
                Missionskillid = addMission.missionSkillId,
                Missionimages = addMission.missionImages,
                Totalsheets = addMission.totalSheets,
                Missionorganisationname = "Default Org",
                Missionorganisationdetail = "Default Details",
                Missiontype = "Short Term",
                Registrationdeadline = DateOnly.FromDateTime(addMission.startDate.AddDays(-1)),
                Missionavilability = "Daily"
            };

            await _context.Missions.AddAsync(mission);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Message = "Mission added successfully",
                    Data = null!
                }
            });
        }

        [Authorize]
        [HttpPost("UpdateMission")]
        public async Task<IActionResult> UpdateMission(AddMissionDto addMission)
        {
            var mission = await _context.Missions.FindAsync(addMission.id);
            if (mission == null)
            {
                return NotFound("Mission not found");
            }

            mission.Missiontitle = addMission.missionTitle;
            mission.Missiondescription = addMission.missionDescription;
            mission.Countryid = addMission.countryId;
            mission.Cityid = addMission.cityId;
            mission.Startdate = DateOnly.FromDateTime(addMission.startDate);
            mission.Enddate = DateOnly.FromDateTime(addMission.endDate);
            mission.Missionthemeid = addMission.missionThemeId;
            mission.Missionskillid = addMission.missionSkillId;
            mission.Totalsheets = addMission.totalSheets;
            mission.Missionimages = addMission.missionImages != null ? string.Join(",", addMission.missionImages) : null;
            mission.Registrationdeadline = DateOnly.FromDateTime(addMission.startDate.AddDays(-1));

            await _context.SaveChangesAsync();


            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Message = "Mission updated successfully",
                    Data = null!
                }
            });

        }

        [Authorize]
        [HttpGet("MissiondetailById/{id}")]
        public async Task<IActionResult> GetMissiondetailById(int id)
        {
            Mission mission = _context.Missions.FirstOrDefault(m => m.Id == id);

            if (mission == null)
                return NotFound("No such mission available");

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Message = "Mission data fetched successfully",
                    Data = mission
                }
            });
        }

        /// <summary>
        /// active msn_skill list 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetMissionSkillList")]
        public async Task<IActionResult> GetMissionSkillList()
        {
            IList<Missionskill> missionskills = await _context.Missionskills.Where(m => m.Status == "Active").ToListAsync();

            return Ok(new ApiResult<IList<Missionskill>>
            {
                result = 1,
                data = new ApiResponse<IList<Missionskill>>
                {
                    Data = missionskills,
                    Message = "Missionskill list fetched successfully"
                }
            });
        }

        /// <summary>
        /// active msn_theme list 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetMissionThemeList")]
        public async Task<IActionResult> GetMissionThemeList()
        {
            IList<Missiontheme> missionthemes = await _context.Missionthemes.Where(m => m.Status == "Active").ToListAsync();

            return Ok(new ApiResult<IList<Missiontheme>>
            {
                result = 1,
                data = new ApiResponse<IList<Missiontheme>>
                {
                    Data = missionthemes,
                    Message = "Missiontheme list fetched successfully"
                }
            });
        }

        [Authorize]
        [HttpGet("MissionApplicationList")]
        public async Task<IActionResult> GetMissionApplicationList()
        {
            IList<MissionApplicationDto> missionapplication = await _context.Missionapplications
                .Include(ma => ma.Mission).Include(ma => ma.User)
                .Select(ma => new MissionApplicationDto
                {
                    Id = ma.Id,
                    Missionid = ma.Missionid,
                    Applieddate = ma.Applieddate,
                    MissionTitle = ma.Mission.Missiontitle,
                    Sheet = ma.Sheet,
                    Status = ma.Status,
                    Userid = ma.Userid,
                    UserName = ma.User.Firstname + " " + ma.User.Lastname,
                    MissionTheme = ma.Mission.Missiontheme!.Themename
                }).ToListAsync();

            return Ok(new ApiResult<IList<MissionApplicationDto>>
            {
                result = 1,
                data = new ApiResponse<IList<MissionApplicationDto>>
                {
                    Data = missionapplication,
                    Message = "Missionapp list fetched successfully"
                }
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("MissionApplicationApprove/{id}")]
        public async Task<IActionResult> ApproveMissionApp(int id)
        {
            var app = await _context.Missionapplications.FindAsync(id);

            if (app == null)
                return NotFound("No such missionapplication available");

            var mission = await _context.Missions.FindAsync(app.Missionid);

            app.Status = true;
            mission.Totalsheets -= app.Sheet;

            await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Data = null!,
                    Message = "Missionapp approved successfully"
                }
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("MissionApplicationDelete/{id}")]
        public async Task<IActionResult> DeleteMissionApp(int id)
        {
            var app = await _context.Missionapplications.FindAsync(id);

            if (app == null)
                return NotFound("No such missionapplication available");

            var mission = await _context.Missions.FindAsync(app.Missionid);

            _context.Missionapplications.Remove(app);
            mission.Totalsheets += app.Sheet;
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Data = null!,
                    Message = "Mission  application deleted successfully"
                }
            });
        }

        [Authorize]
        [HttpDelete("DeleteMission/{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            Mission mission = await _context.Missions.FindAsync(id);

            if (mission == null)
                return NotFound("No such mission for delete availabale");

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Data = null!,
                    Message = "Mission deleted successfully"
                }
            });


        }



    }
}
