using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using VirtualCommunitySupportWebApi.Data;
using VirtualCommunitySupportWebApi.Models;
using VirtualCommunitySupportWebApi.Models.DTOS;
using VirtualCommunitySupportWebApi.Util;

namespace VirtualCommunitySupportWebApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class AdminUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminUserController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("UserDetailList")]
        public async Task<IActionResult> GetUserList()
        {
            var userInfoList = _context.Users.Select(user => new UserInfo
            {
                id = user.Id,
                emailAddress = user.Emailaddress,
                firstName = user.Firstname,
                lastName = user.Lastname,
                userType = user.Usertype,
                phoneNumber = user.Phonenumber
            }).ToArray();

            return Ok(new ApiResult<UserInfo[]>
            {
                result = 1,
                data = new ApiResponse<UserInfo[]>
                {
                    Data = userInfoList,
                    Message = "User list retrieved"
                }
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existuser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (existuser == null)
                return NotFound("No such user.");

            _context.Users.Remove(existuser);
            await _context.SaveChangesAsync();


            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<Object>
                {
                    Data = null!,
                    Message = "User deleted successfully"
                }
            });
        }
    }
}
