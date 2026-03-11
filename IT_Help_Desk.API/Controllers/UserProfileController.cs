using IT_Help_Desk.Application.Tickets.Queries.GetTicketById;
using IT_Help_Desk.Application.UsersProfiles.Commands.CreateProfileCommand;
using IT_Help_Desk.Application.UsersProfiles.Commands.UpdateProfileCommands;
using IT_Help_Desk.Application.UsersProfiles.DTOs;
using IT_Help_Desk.Application.UsersProfiles.Queries;
using IT_Help_Desk.Application.UsersProfiles.Queries.GetProfileById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IT_Help_Desk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController(IMediator mediator) : ControllerBase
    {
        // CREATE PROFILE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserProfileDTO dto)
        {
            try
            {
                // تحقق من البيانات
                if (dto == null)
                    return BadRequest(new { message = "Invalid profile data" });

                // استخراج UserId من JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                    return Unauthorized(new { message = "Invalid token" });

                var userId = int.Parse(userIdClaim.Value);

                // إرسال command
                var result = await mediator.Send(new CreateUserProfileCommand
                {
                    UserId = userId,
                    Data = dto
                });

                return Ok(new
                {
                    message = "Profile created successfully",
                    profile = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to create profile",
                    detail = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [Authorize]
        [HttpGet("All-Profiles")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            var userId = int.Parse(userIdClaim.Value);

            var result = await mediator.Send(new GetMyProfileQueries
            {
                UserId = userId
            });

            if (result == null)
                return NotFound(new { message = "Profile not found" });

            return Ok(new
            {
                message = "Profile retrieved successfully",
                data = result
            });
        }

       

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UserProfileDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            var userId = int.Parse(userIdClaim.Value);

            var result = await mediator.Send(new UpdateUserProfileCommand
            {
                UserId = userId,
                Data = dto
            });

            return Ok(new
            {
                message = "Profile updated successfully",
                data = result
            });
        }

    }
}

