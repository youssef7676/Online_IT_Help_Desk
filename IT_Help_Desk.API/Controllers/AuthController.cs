using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.Tickets.Queries;
using IT_Help_Desk.Application.Tickets.Queries.GetTicketById;
using IT_Help_Desk.Application.Users.Commands.ChangeRole;
using IT_Help_Desk.Application.Users.Commands.ChangeStatus;
using IT_Help_Desk.Application.Users.Commands.RefreshToken;
using IT_Help_Desk.Application.Users.Commands.UserLogin;
using IT_Help_Desk.Application.Users.Commands.UsersRigster;
using IT_Help_Desk.Application.Users.DTOs;
using IT_Help_Desk.Application.Users.Queries.GetAllUser;
using IT_Help_Desk.Application.Users.Queries.GetAllUserById;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IT_Help_Desk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediat, IMapper mapper, IUserRepository repo, IJwtService jwtService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(RigsterDTO dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Invalid registration data" });

            try
            {
                var result = await mediat.Send(new CreateRigsterCommand { Data = dto });
                return Ok(new { message = "User registered successfully", user = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Registration failed",
                    detail = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] IT_Help_Desk.Application.Users.DTOs.LoginDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest(new { message = "Email and password are required" });

            try
            {
                // تحويل DTO للكوماند
                var loginCommandDto = new IT_Help_Desk.Application.Users.Commands.UserLogin.LoginDTO
                {
                    Email = dto.Email,
                    Password = dto.Password
                };

                // تنفيذ Login Command
                var loginResponse = await mediat.Send(new CreateLoginCommand { Data = loginCommandDto });
                if (loginResponse == null)
                    return Unauthorized(new { message = "Invalid email or password" });

                // توليد Refresh Token
                loginResponse.RefreshToken = jwtService.GenerateRefreshToken();
                loginResponse.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                //  أهم تعديل: جلب الـ User مع Profile
                var userEntity = await repo.GetByEmailWithProfile(dto.Email); // Include(UserProfile)
                if (userEntity == null)
                    return NotFound(new { message = "User not found" });

                // تحديث Refresh Token في DB
                userEntity.RefreshToken = loginResponse.RefreshToken;
                userEntity.RefreshTokenExpiryTime = loginResponse.RefreshTokenExpiryTime;
                repo.Update(userEntity);

                // حفظ Refresh Token في Cookie
                SetRefreshTokenCookie(loginResponse.RefreshToken, loginResponse.RefreshTokenExpiryTime);

                // إعادة الـ User مع Profile
                return Ok(new
                {
                    message = "Login successful",
                    accessToken = loginResponse.Token,
                    user = mapper.Map<UserDTO>(userEntity) // Profile هيتعبّي أوتوماتيك
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login", detail = ex.Message });
            }
        }




        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new { message = "No refresh token provided" });

            var loginResponse = await mediat.Send(new RefreshTokenCommand { RefreshToken = refreshToken });
            if (loginResponse == null)
                return Unauthorized(new { message = "Invalid or expired refresh token" });

            // حفظ Refresh Token جديد في Cookie
            SetRefreshTokenCookie(loginResponse.RefreshToken, loginResponse.RefreshTokenExpiryTime);

            return Ok(new
            {
                accessToken = loginResponse.Token,
                user = loginResponse.User
            });
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var user = await repo.GetByRefreshToken(refreshToken);
                if (user != null)
                {
                    user.RefreshToken = null;
                    user.RefreshTokenExpiryTime = DateTime.MinValue;
                    repo.Update(user);
                }
            }

            Response.Cookies.Delete("refreshToken");
            return Ok(new { message = "Logged out successfully" });
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await mediat.Send(new GetAllUsersQuery());

                if (users == null || !users.Any())
                {
                    return Ok(new
                    {
                        message = "No users found",
                        users = new List<UserDTO>()
                    });
                }

                return Ok(new
                {
                    message = "Users retrieved successfully",
                    users = users
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to retrieve users",
                    detail = ex.Message
                });
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                // استدعي الـ Handler اللي فيه Include للـ Profile
                var response = await mediat.Send(new GetUserByIdQuery(id));

                if (response == null)
                    return NotFound(new { message = "User not found" });

                return Ok(new
                {
                    message = "User retrieved successfully",
                    user = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while fetching user",
                    detail = ex.Message
                });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Change-Role")]
        public async Task<IActionResult> ChangeRole(ChangeRoleCommand command)
        {
            try
            {
                var result = await mediat.Send(command);
                return Ok(new { message = "User role updated successfully", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to change user role", detail = ex.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Change-Status")]
        public async Task<IActionResult> ChangeStatus(ChangeUserStatusCommand command)
        {
            try
            {
                var result = await mediat.Send(command);
                return Ok(new { message = "User status updated successfully", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to change user status", detail = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await repo.DeleteByIdWithProfile(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            await repo.Delete(user.Id);
            await repo.Save();

            return Ok(new { message = "User deleted successfully" });
        }


        private void SetRefreshTokenCookie(string refreshToken, DateTime expiry)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // فقط HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = expiry
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}




//using AutoMapper;
//using IT_Help_Desk.Application.Users.Commands.RefreshToken;
//using IT_Help_Desk.Application.Users.Commands.UserLogin;
//using IT_Help_Desk.Application.Users.Commands.UsersRigster;
//using IT_Help_Desk.Application.Users.DTOs;
//using IT_Help_Desk.Application.Users.Queries.GetAllUser;
//using IT_Help_Desk.Application.Users.Queries.GetAllUserById;
//using IT_Help_Desk.Application.UsersProfiles.Commands.CreateProfileCommand;
//using IT_Help_Desk.Domain.Interfaces;
//using IT_Help_Desk.Infrastruction.Reposatories;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace IT_Help_Desk.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController(IMediator mediat, IMapper mapper, IUserRepository userRepo, IUserProfileReposatory profileRepo, IJwtService jwtService) : ControllerBase
//    {
//        // ========================= Register =========================
//        [HttpPost("register")]
//        public async Task<IActionResult> Register(RigsterDTO dto)
//        {
//            if (dto == null) return BadRequest(new { message = "Invalid registration data" });

//            try
//            {
//                var userResult = await mediat.Send(new CreateRigsterCommand { Data = dto });

//                // Create Profile automatically
//                var profileResult = await mediat.Send(new CreateUserProfileCommand
//                {
//                    UserId = userResult.Id,
//                    Data = new IT_Help_Desk.Application.UsersProfiles.DTOs.UserProfileDTO
//                    {
//                        Department = "Not Set",
//                        JobTitle = "Not Set",
//                        PhoneNumber = "Not Set"
//                    }
//                });

//                return Ok(new
//                {
//                    message = "User registered successfully",
//                    user = userResult,
//                    profile = profileResult
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new
//                {
//                    message = "Registration failed",
//                    detail = ex.Message,
//                    inner = ex.InnerException?.Message
//                });
//            }
//        }

//        // ========================= Login =========================

//        [AllowAnonymous]
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] IT_Help_Desk.Application.Users.DTOs.LoginDTO dto)
//        {
//            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
//                return BadRequest(new { message = "Email and password are required" });

//            try
//            {
//                var loginCommandDto = new IT_Help_Desk.Application.Users.Commands.UserLogin.LoginDTO
//                {
//                    Email = dto.Email,
//                    Password = dto.Password
//                };

//                var loginResponse = await mediat.Send(new CreateLoginCommand { Data = loginCommandDto });
//                if (loginResponse == null)
//                    return Unauthorized(new { message = "Invalid email or password" });

//                loginResponse.RefreshToken = jwtService.GenerateRefreshToken();
//                loginResponse.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

//                var userEntity = await userRepo.GetById(loginResponse.User.Id);
//                if (userEntity == null)
//                    return NotFound(new { message = "User not found" });

//                userEntity.RefreshToken = loginResponse.RefreshToken;
//                userEntity.RefreshTokenExpiryTime = loginResponse.RefreshTokenExpiryTime;
//                userRepo.Update(userEntity);

//                SetRefreshTokenCookie(loginResponse.RefreshToken, loginResponse.RefreshTokenExpiryTime);

//                return Ok(new
//                {
//                    message = "Login successful",
//                    accessToken = loginResponse.Token,
//                    user = loginResponse.User
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "An error occurred during login", detail = ex.Message });
//            }
//        }

//        // ========================= Refresh Token =========================
//        [HttpPost("refresh-token")]
//        [AllowAnonymous]
//        public async Task<IActionResult> RefreshToken()
//        {
//            var refreshToken = Request.Cookies["refreshToken"];
//            if (string.IsNullOrEmpty(refreshToken)) return Unauthorized(new { message = "No refresh token provided" });

//            var loginResponse = await mediat.Send(new RefreshTokenCommand { RefreshToken = refreshToken });
//            if (loginResponse == null) return Unauthorized(new { message = "Invalid or expired refresh token" });

//            SetRefreshTokenCookie(loginResponse.RefreshToken, loginResponse.RefreshTokenExpiryTime);

//            return Ok(new
//            {
//                accessToken = loginResponse.Token,
//                user = loginResponse.User
//            });
//        }

//        // ========================= Logout =========================
//        [HttpPost("logout")]
//        [Authorize]
//        public async Task<IActionResult> Logout()
//        {
//            var refreshToken = Request.Cookies["refreshToken"];
//            if (!string.IsNullOrEmpty(refreshToken))
//            {
//                var user = await userRepo.GetByRefreshToken(refreshToken);
//                if (user != null)
//                {
//                    user.RefreshToken = null;
//                    user.RefreshTokenExpiryTime = null;
//                    userRepo.Update(user);
//                    await userRepo.Save();
//                }
//            }

//            Response.Cookies.Delete("refreshToken");
//            return Ok(new { message = "Logged out successfully" });
//        }

//        // ========================= Get All Users (Admin) =========================
//        [Authorize(Roles = "Admin")]
//        [HttpGet]
//        public async Task<IActionResult> AllUsers()
//        {
//            try
//            {
//                var users = await userRepo.GetAllUserWithProfile(u => u.Profile);
//                var response = mapper.Map<List<UserDTO>>(users);

//                if (response == null || !response.Any())
//                    return Ok(new { message = "No users found", users = new List<UserDTO>() });

//                return Ok(new { message = "Users retrieved successfully", users = response });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "An error occurred while fetching users", detail = ex.Message });
//            }
//        }

//        // ========================= Get User by Id (Admin) =========================
//        [Authorize(Roles = "Admin")]
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUserById(int id)
//        {
//            try
//            {
//                var response = await mediat.Send(new GetUserByIdQuery(id));
//                if (response == null)
//                    return NotFound(new { message = "User not found" });

//                return Ok(new { message = "User retrieved successfully", user = response });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "An error occurred while fetching user", detail = ex.Message });
//            }
//        }

//        // ========================= Private =========================
//        private void SetRefreshTokenCookie(string refreshToken, DateTime expiry)
//        {
//            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
//            {
//                HttpOnly = true,
//                Secure = true,
//                SameSite = SameSiteMode.Strict,
//                Expires = expiry
//            });
//        }
//    }
//}

