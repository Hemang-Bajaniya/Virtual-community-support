using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualCommunitySupportWebApi.Data;
using VirtualCommunitySupportWebApi.Models.DTOS;
using VirtualCommunitySupportWebApi.Util;

namespace VirtualCommunitySupportWebApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommonController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize]
        [HttpGet("CountryList")]
        public async Task<IActionResult> GetCountryList()
        {
            IList<CountryDto> countries = await _context.Countries.Select(c => new CountryDto { id = c.Id, name = c.Countryname }).ToListAsync();

            return Ok(new ApiResult<IList<CountryDto>>
            {
                result = 1,
                data = new ApiResponse<IList<CountryDto>>
                {
                    Data = countries,
                    Message = "Countrylist fetched successfully"
                }
            });
        }

        [Authorize]
        [HttpGet("CityList/{id}")]
        public async Task<IActionResult> CityList(int id)
        {
            IList<CityDto> cities = await _context.Cities.Where(c => c.Countryid == id).Select(c => new CityDto { id = c.Id, name = c.Cityname }).ToListAsync();

            return Ok(new ApiResult<IList<CityDto>>
            {
                result = 1,
                data = new ApiResponse<IList<CityDto>>
                {
                    Data = cities,
                    Message = $"Citylist for country#{id} fetched successfully"
                }
            });
        }

        [Authorize]
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> file)
        {
            if (file == null || file.Count == 0)
                return BadRequest(new { success = false, message = "No images provided", d = file });

            var filePaths = new List<string>();
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var img in file)
            {
                var name = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                var path = Path.Combine(uploadPath, name);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                filePaths.Add($"uploads/{name}");
            }

            return Ok(new { success = true, data = filePaths });
        }

        [Authorize]
        [HttpGet("GetUserSkill/{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            var skills = await _context.Userskills.FirstOrDefaultAsync(s => s.Userid == id);

            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Data = skills,
                    Message = "userskiils fetched"
                }
            }
                );

        }

    }
}
