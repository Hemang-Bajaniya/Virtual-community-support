using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using VirtualCommunitySupportWebApi.Data;
using VirtualCommunitySupportWebApi.Models;
using VirtualCommunitySupportWebApi.Models.DTOS;
using VirtualCommunitySupportWebApi.Util;

namespace VirtualCommunitySupportWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public LoginController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            if (_context.Users.Any(u => u.Emailaddress == user.emailAddress))
                return Ok(new { result = -1, data = new ApiResponse<Object> { Message = "Account with same email already exits", Data = null! } });
            //return BadRequest("Account with same email already exits");

            var newUser = new User
            {
                Emailaddress = user.emailAddress,
                Firstname = user.firstName,
                Lastname = user.lastName,
                Phonenumber = user.phoneNumber,
                Password = user.password,
                Usertype = user.userType,
            };


            // hash pass
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { result = 1, data = new ApiResponse<Object> { Message = "User registred Successfully", Data = null! } });
        }


        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            Console.WriteLine($"[LoginController] Incoming LoginDto: Email={user.EmailAddress}, Pass={user.Password}");
            var existuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Emailaddress == user.EmailAddress && u.Password == user.Password);

            Console.WriteLine(existuser);

            if (existuser == null)
                return Unauthorized("Invalid credentials");

            Console.WriteLine(existuser);
            var token = _jwtService.GenerateToken(existuser);
            Console.WriteLine(token);

            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Ok(new { result = 1, data = new ApiResponse<Object> { Message = "Login Successfully", Data = token } });
        }

        //by post idk
        [Authorize(Roles = "Admin")]
        [HttpGet("/api/getAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(new ApiResponse<List<User>> { Message = "Users data fetched successfully", Data = users });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/getAllUsersFileters")]
        public async Task<IActionResult> GetFilteredUsers([FromBody] FilterModel filtrerModel)
        {
            if (filtrerModel == null)
                return BadRequest(new ApiResponse<object> { Message = "Invalid filter model", Data = null! });

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtrerModel.SearchString))
            {
                query = query.Where(u =>
                u.Firstname.ToLower().Contains(filtrerModel.SearchString.ToLower()) ||
                u.Lastname.ToLower().Contains(filtrerModel.SearchString.ToLower()) ||
                u.Emailaddress.ToLower().Contains(filtrerModel.SearchString.ToLower()));
            }

            query = query.OrderBy(u => u.Firstname.ToLower()).ThenBy(u => u.Lastname.ToLower());

            var pgSiz = filtrerModel.PageSize > 0 ? filtrerModel.PageSize : 4;
            var pgNo = filtrerModel.PageNumber > 0 ? filtrerModel.PageNumber : 1;
            var totalRecords = await _context.Users.CountAsync();

            var pagiUsers = await query.Skip((pgNo - 1) * pgSiz).Take(pgSiz).ToListAsync();

            return Ok(new ApiResponse<Object>
            {
                Message = "Users data fetched successfully",
                Data = new
                {
                    Users = pagiUsers,
                    TotalRecords = totalRecords,
                    PageNumber = pgNo,
                    PageSize = pgSiz,
                    TotalPages = (int)Math.Ceiling((double)totalRecords / pgSiz)
                }
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromForm] UpdateUserDto user)
        {
            Console.WriteLine(user);
            var existuser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (existuser == null)
                return NotFound("No such user.");

            existuser.Firstname = user.firstName;
            existuser.Lastname = user.lastName;
            existuser.Phonenumber = user.phoneNumber;
            existuser.Emailaddress = user.emailAddress;

            await _context.SaveChangesAsync();
            return Ok(new ApiResult<Object>
            {
                result = 1,
                data = new ApiResponse<object>
                {
                    Data = null!,
                    Message = "User Updated successfully"
                }
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddUser(AdminRegisterDto user)
        {
            if (_context.Users.Any(u => u.Emailaddress == user.emailAddress))
                return BadRequest("Account with this email already exists.");

            var newUser = new User
            {
                Firstname = user.firstName,
                Lastname = user.lastName,
                Emailaddress = user.emailAddress,
                Password = user.password,
                Phonenumber = user.phoneNumber,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<RegisterDto> { Message = "User added successfully", Data = user });

        }

        [Authorize]
        [HttpGet("LoginUserDetailById/{id}")]
        public async Task<IActionResult> GetCurrentUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new ApiResult<Object>
                {
                    result = 0,
                    data = new ApiResponse<Object> { Message = "User not found", Data= null! }
                });
            }

            UserInfo data = new UserInfo
            {
                emailAddress = user.Emailaddress,
                firstName = user.Firstname,
                lastName = user.Lastname,
                userType = user.Usertype,
                phoneNumber = user.Phonenumber
            };

            return Ok(new ApiResult<UserInfo>
            {
                result = 1,
                data = new ApiResponse<UserInfo>
                {
                    Data = data,
                    Message = "User data retrieved"
                }
            });
        }

        //[Authorize]
        //[HttpGet("profile")]
        //public async Task<IActionResult> UserProfile()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var loggedUser = await _context.Users.FindAsync(int.Parse(userId));

        //    if (loggedUser == null)
        //        return NotFound(new ApiResponse<User> { Message = "No such user avilable", Data = null! });

        //    return Ok(new ApiResponse<object>
        //    {
        //        Message = "Profile data fetched successful",
        //        Data = new
        //        {
        //            FirstName = loggedUser.Firstname,
        //            LastName = loggedUser.Lastname,
        //            Email = loggedUser.Emailaddress,
        //            PhoneNumber = loggedUser.Phonenumber
        //        }
        //    });
        //}

        [Authorize]
        [HttpGet("GetUserProfileDetailById/{id}")]
        public async Task<IActionResult> UserProfile(int id)
        {
            var details = _context.Userdetails.Include(d => d.Country).Where(d => d.Userid == id);

            return Ok(new ApiResult<object>
            {
                result = 1,
                data = new ApiResponse<object>
                { 
                    Data = details,
                    Message = "User details fetched"
                }

            });
        }

        public class UserProfileUpdateDto
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string EmployeeId { get; set; }
            public string Manager { get; set; }
            public string Title { get; set; }
            public string Department { get; set; }
            public string MyProfile { get; set; }
            public string WhyIVolunteer { get; set; }
            public int? CountryId { get; set; }
            public int? CityId { get; set; }
            public string Avilability { get; set; }
            public string LinkdInUrl { get; set; }
            public string MySkills { get; set; }
            public string UserImage { get; set; }
            public bool Status { get; set; }
        }

        [Authorize]
        [HttpPost("LoginUserProfileUpdate")]
        public async Task<IActionResult> LoginUserProfileUpdate([FromBody] UserProfileUpdateDto dto)
        {
            if (dto == null || dto.UserId == 0)
                return BadRequest("Invalid user data.");

            var userDetail = await _context.Userdetails
                .FirstOrDefaultAsync(u => u.Userid == dto.UserId);

            if (userDetail == null)
                return NotFound("User profile not found.");

            // Update fields
            userDetail.Name = dto.Name;
            userDetail.Surname = dto.Surname;
            userDetail.Employeeid = dto.EmployeeId;
            userDetail.Manager = dto.Manager;
            userDetail.Title = dto.Title;
            userDetail.Department = dto.Department;
            userDetail.Myprofile = dto.MyProfile;
            userDetail.Whyivolunteer = dto.WhyIVolunteer;
            userDetail.Countryid  = dto.CountryId;
            userDetail.Cityid = dto.CityId;
            userDetail.Avilability = dto.Avilability;
            userDetail.Linkdinurl = dto.LinkdInUrl;
            userDetail.Myskills = dto.MySkills;
            userDetail.Userimage = dto.UserImage;
            userDetail.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(new { result = 1, data = "Profile updated successfully." });
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChnagePassword(ChnagePassDto dto)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId && u.Password == dto.OldPassword);

            if (user == null)
                return NotFound("No such user available");

            user.Password = dto.NewPassword;

            await _context.SaveChangesAsync();

            return Ok(new { result = 1, data = "Password changed successfully" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("token");

            return Ok(new ApiResponse<Object> { Message = "Logout successful", Data = null! });
        }
    }


}
