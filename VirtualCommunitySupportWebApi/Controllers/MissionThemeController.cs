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
    public class MissionThemeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MissionThemeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get msn_theme list
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetMissionThemeList")]
        public async Task<IActionResult> GetMissionThemeList()
        {
            IList<Missiontheme> missionthemes = await _context.Missionthemes.ToListAsync();

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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="missiontheme"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddMissionTheme")]
        public async Task<IActionResult> AddMissionTheme([FromBody] MissionThemeInfo missionThemeInfo)
        {
            bool isExists = await _context.Missionthemes.AnyAsync(m => m.Themename.ToLower() == missionThemeInfo.themeName.ToLower());

            if (isExists)
                return BadRequest("MissionTheme already avilable");

            await _context.Missionthemes.AddAsync(new Missiontheme { Themename = missionThemeInfo.themeName, Status = missionThemeInfo.status});
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object> { result = 1, data = new ApiResponse<object> { Data = null, Message = "MissionTheme added" } });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetMissionThemeById/{id}")]
        public async Task<IActionResult> GetMissionThemeById(int id)
        {
            Missiontheme missiontheme = await _context.Missionthemes.FirstOrDefaultAsync(m => m.Id == id);

            MissionThemeInfo missionThemeInfo = new MissionThemeInfo
            {
                id = missiontheme.Id,
                themeName = missiontheme.Themename,
                status = missiontheme.Status
            };

            if (missiontheme == null)
                return BadRequest("No such missiontheme available");

            return Ok(new ApiResult<MissionThemeInfo>
            {
                result = 1,
                data = new ApiResponse<MissionThemeInfo> { Data = missionThemeInfo, Message = "Missiontheme data fetched successfully" }
            });
        }


        [Authorize]
        [HttpPost("updateMissionTheme")]
        public async Task<IActionResult> UpdateMissionThemeById([FromBody] MissionThemeInfo missionThemeInfo)
        {
            Console.WriteLine(missionThemeInfo);
            Missiontheme missiontheme = await _context.Missionthemes.FirstOrDefaultAsync(m => m.Id == missionThemeInfo.id);

            if (missiontheme == null)
                return BadRequest(new { m = "No such missiontheme for update available", d = missionThemeInfo });

            missiontheme.Themename = missionThemeInfo.themeName;
            missiontheme.Status = missionThemeInfo.status;

            await _context.SaveChangesAsync();

            return Ok(new ApiResult<MissionThemeInfo>
            {
                result = 1,
                data = new ApiResponse<MissionThemeInfo> { Data = missionThemeInfo, Message = "Missiontheme data updated successfully" }
            });
        }


        /// <summary>
        /// delete msn_theme by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("DeleteMissionTheme/{id}")]
        public async Task<IActionResult> DeleteMissionThemeById(int id)
        {
            Missiontheme missiontheme = await _context.Missionthemes.FirstOrDefaultAsync(m => m.Id == id);

            if (missiontheme == null)
                return BadRequest("No such missiontheme available");

            _context.Missionthemes.Remove(missiontheme);
           await _context.SaveChangesAsync();

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Data = null!,
                    Message = "Missiontheme deleted successfully"
                }
            });
        }

    }
}
