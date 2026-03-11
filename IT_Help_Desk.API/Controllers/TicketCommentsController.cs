using AutoMapper;
using IT_Help_Desk.Application.TicketsComments.Commands.CreateTicketCommentsCommands;
using IT_Help_Desk.Application.TicketsComments.DTOs;
using IT_Help_Desk.Application.TicketsComments.Queries;
using IT_Help_Desk.Application.TicketsComments.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IT_Help_Desk.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketCommentsController(IMediator mediat, IMapper mapper) : ControllerBase
    {
        // =========================
        // كل التعليقات (Admin / IT فقط)
        // =========================
        [Authorize(Roles = "Admin,IT")]
        [HttpGet]
        public async Task<IActionResult> AllComments()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (roleClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            // تحقق من وجود الدور في القائمة المسموح بها
            var allowedRoles = new List<string> { "Admin", "IT" };
            if (!allowedRoles.Contains(roleClaim.Value))
                return StatusCode(403, new { message = "You do not have permission to view comments" });

            var response = await mediat.Send(new GetAllCommentsQuery());

            if (response == null || !response.Any())
                return Ok(new { message = "No comments found", comments = new List<object>() });

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentsById(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized(new { message = "Invalid or missing token" });

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            var response = await mediat.Send(new GetCommentsByIdQuery(id));

            if (response == null)
                return NotFound(new { message = "Comment not found" });

            // User يرى فقط تعليقات تذكرته
            if (role == "User" && response.TicketId != userId)
                return StatusCode(403, new { message = "You do not own this ticket's comment" });

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentsCommands obj)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized(new { message = "Invalid or missing token" });

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            // User لا يضيف Comment إلا على تذكرته
            if (role == "User")
                obj.CreatedById = userId;

            var response = await mediat.Send(obj);

            return Ok(new { message = "Comment added successfully", commentId = response });
        }


    }
}
