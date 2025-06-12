using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualCommunitySupportWebApi.Data;
using VirtualCommunitySupportWebApi.Models;
using VirtualCommunitySupportWebApi.Models.DTOS;
using VirtualCommunitySupportWebApi.Util;

namespace VirtualCommunitySupportWebApi.Controllers
{
    [Route("api/[controller]")]
    public class MissionSkillController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MissionSkillController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetMissionSkillList")]
        public async Task<IActionResult> GetMissionSkillList()
        {
            IList<Missionskill> missionskills = await _context.Missionskills.ToListAsync();

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
        /// 
        /// </summary>
        /// <param name="missionskill"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddMissionSkill")]
        public async Task<IActionResult> AddMissionSkill([FromBody] MissionSkillInfo missionskillinfo)
        {
            bool isExists = await _context.Missionskills.AnyAsync(m => m.Skillname.ToLower() == missionskillinfo.skillName.ToLower());

            if (isExists)
                return BadRequest("MissionSkill already avilable");

            await _context.Missionskills.AddAsync(new Missionskill { Skillname = missionskillinfo.skillName, Status = missionskillinfo.status, Missions = [] });
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object> { result = 1, data = new ApiResponse<object> { Data = null, Message = "MissionSkill added" } });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetMissionSkillById/{id}")]
        public async Task<IActionResult> GetMissionSkillById(int id)
        {
            Missionskill missionskill = await _context.Missionskills.FirstOrDefaultAsync(m => m.Id == id);

            MissionSkillInfo missionSkillInfo = new MissionSkillInfo
            {
                id = missionskill.Id,
                skillName = missionskill.Skillname,
                status = missionskill.Status
            };

            if (missionskill == null)
                return BadRequest("No such missionskill available");

            return Ok(new ApiResult<MissionSkillInfo>
            {
                result = 1,
                data = new ApiResponse<MissionSkillInfo> { Data = missionSkillInfo, Message = "Missionskill data fetched successfully" }
            });
        }


        [Authorize]
        [HttpPost("updateMissionSkill")]
        public async Task<IActionResult> UpdateMissionSkillById([FromBody] MissionSkillInfo missionSkillInfo)
        {
            Console.WriteLine(missionSkillInfo);
            Missionskill missionskill = await _context.Missionskills.FirstOrDefaultAsync(m => m.Id == missionSkillInfo.id);

            if (missionskill == null)
                return BadRequest(new { m = "No such missionskill for update available", d = missionSkillInfo });

            missionskill.Skillname = missionSkillInfo.skillName;
            missionskill.Status = missionSkillInfo.status;

            await _context.SaveChangesAsync();

            return Ok(new ApiResult<MissionSkillInfo>
            {
                result = 1,
                data = new ApiResponse<MissionSkillInfo> { Data = missionSkillInfo, Message = "Missionskill data updated successfully" }
            });
        }


        /// <summary>
        /// delete msn_skill by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("DeleteMissionSkill/{id}")]
        public async Task<IActionResult> DeleteMissionSkillById(int id)
        {
            Missionskill missionskill = await _context.Missionskills.FirstOrDefaultAsync(m => m.Id == id);

            if (missionskill == null)
                return BadRequest("No such missionskill available");

            _context.Missionskills.Remove(missionskill);
           await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Data = null!,
                    Message = "Missionskill deleted successfully"
                }
            });
        }

    }
}
