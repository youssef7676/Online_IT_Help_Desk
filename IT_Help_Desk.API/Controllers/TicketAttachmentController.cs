using AutoMapper;
using IT_Help_Desk.Application.TicketsAttachments.Commands.AttachmentCommands;
using IT_Help_Desk.Application.TicketsAttachments.DTOs;
using IT_Help_Desk.Application.TicketsAttachments.Queries.AttachmentQuery;
using IT_Help_Desk.Application.TicketsAttachments.Queries.AttchmentQueryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IT_Help_Desk.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketAttachmentController(IMediator mediat, IMapper mapper) : ControllerBase
    {

        [Authorize(Roles = "Admin,IT")]
        [HttpGet]
        public async Task<IActionResult> AllAttachment()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (roleClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            // تحقق من وجود الدور في القائمة المسموح بها
            var allowedRoles = new List<string> { "Admin", "IT" };
            if (!allowedRoles.Contains(roleClaim.Value))
                return StatusCode(403, new { message = "You do not have permission to view attachments" });

            var response = await mediat.Send(new GetTicketAttachmentQuery());

            if (response == null || !response.Any())
                return Ok(new { message = "No attachments found", attachments = new List<object>() });

            return Ok(response);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttachmentById(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized(new { message = "Invalid or missing token" });

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            var response = await mediat.Send(new GetAttachmentByIdQuery(id));

            if (response == null)
                return NotFound(new { message = "Attachment not found" });

            // User يرى فقط Attachments الخاصة بتذكرته
            if (role == "User" && response.TicketId != userId)
                return StatusCode(403, new { message = "You do not own this ticket's attachment" });

            return Ok(response);
        }



        [HttpPost]
        public async Task<IActionResult> Upload(int ticketId, IFormFile file)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized(new { message = "Invalid or missing token" });

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file provided" });

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var command = new CreateAttachmentCommands
            {
                TicketId = ticketId,
                FileName = file.FileName,
                FileData = ms.ToArray()
            };

            // User لا يرفع إلا على تذكرته
            if (role == "User")
                command.CreatedById = userId;

            var id = await mediat.Send(command);

            return Ok(new { message = "Attachment uploaded successfully", attachmentId = id });
        }

    }
}
