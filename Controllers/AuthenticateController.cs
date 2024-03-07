using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AMWService.IdentityAuth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AMWService.Models;
using AMWService.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AMWService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly DbConfig _context;

        public AuthenticateController(UserManager<User>userManager, IConfiguration configuration,DbConfig context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Register")]
        //[Route("Register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists!" });
            }

            User user = new User()
            {
                //UserID = model.UserID,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirsName = model.FirsName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                DepartmentID = model.DepartmentID,
                UserRoleID = model.UserRoleID
            };

            var result = await _userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Creation Failed!" });
            }
            return Ok(new Response { Status = "Success", Message = "User Create Successfully!" });
        }

        [HttpPut("EditUser")]
        public async Task<IActionResult> Edituser([FromForm] User model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Not Found" });
            }
            if (model.Status == 2)
            {
                user.Status = model.Status;
            }
            else
            {
                //SecurityStamp = Guid.NewGuid().ToString(),
                //UserName = model.Username,
                user.FirsName = model.FirsName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.DepartmentID = model.DepartmentID;
                user.UserRoleID = model.UserRoleID;
            };

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Edit Failed!" });
            }
            return Ok(new Response { Status = "Success", Message = "User Edit Successfully!" });
        }


        //[HttpPost("ChangePassword")]
        //public async Task<IActionResult> ChangePassword([FromBody] ResetPassword model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.Id);
        //    if (user == null)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Not Found" });
        //    }

        //    var result = await _userManager.ChangePasswordAsync(user, model.Password, model.ConfirmPassword);
        //    if (!result.Succeeded)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Change Password Failed!" });
        //    }
        //    return Ok(new Response { Status = "Success", Message = "Change Password Success" });
        //}

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Not Found" });
            }
            var result = await _userManager.RemovePasswordAsync(user);
            var newpass = await _userManager.AddPasswordAsync(user, model.ConfirmPassword);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Remove Password Failed!" });
            }
            if (!newpass.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Add New Password Failed!" });
            }
            return Ok(new Response { Status = "Success", Message = "Change Password Success" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var uid = await _context.mst_Employee.FirstOrDefaultAsync(x => x.EP_Username == model.Username);
            //var role = await _context.mst_User_Roles.FindAsync(user.UserID);
            if (user.Status == 2)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Username is disabled, Please contact Call Center" });
            }
            if (user != null && user.Status == 1 && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authCleaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name ,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authCleaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authCleaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new {UserID = uid.ID, User = user.UserName,Email = user.Email, Department = user.DepartmentID,user.UserRoleID, accessToken = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo, status = "Success"});
                //UserID = user.UserID
            }
            return Unauthorized(new Response { Message = "Invalid username or password" });
        }

        [HttpPost]

        [HttpGet("CheckPermission")]
        public async Task<ActionResult<IEnumerable<User_Roles>>> CheckPermission([FromQuery] int Role_ID )
        {
            //var result = await _context.mst_User_Role.FindAsync(code.User_ID);
            var result = (await _context.mst_User_Roles.Select(x => new User_Roles()
            {
                ID = x.ID,
                UR_Name_Full = x.UR_Name_Full,
                UR_New_Status = x.UR_New_Status,
                UR_BackLog_Status = x.UR_BackLog_Status,
                UR_Todo_Status = x.UR_Todo_Status,
                UR_Doing_Status = x.UR_Doing_Status,
                UR_Resolved_Status = x.UR_Resolved_Status,
                UR_Done_Status = x.UR_Done_Status,
                UR_Charge_Status = x.UR_Charge_Status,
                UR_Close_Status = x.UR_Close_Status,
                UR_Reject_Status = x.UR_Reject_Status
            }).ToListAsync()).Where(x => x.ID == Role_ID).ToList();

            return Ok(result);
            
        }
    }
}
