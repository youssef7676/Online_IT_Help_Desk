using AutoMapper;
using IT_Help_Desk.Application.Tickets.Commands.CreateTicketCommands;
using IT_Help_Desk.Application.Tickets.Commands.UpdateTicketCommands;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.Tickets.Queries;
using IT_Help_Desk.Application.Tickets.Queries.GetTicketById;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IT_Help_Desk.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController(IMediator mediat, IMapper mapper) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AllTickets()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null || roleClaim.Value != "Admin")
                return StatusCode(403, new { message = "You do not have permission to view all tickets" });

            var query = new GetAllQuery();
            var response = await mediat.Send(query);

            if (response == null || !response.Any())
                return Ok(new { message = "No tickets found", tickets = new List<object>() });

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized(new { message = "Invalid or missing token" });

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            var response = await mediat.Send(new GetTicketByIdQuery(id));

            if (response == null)
                return NotFound(new { message = "Ticket not found" });

            // User لا يرى إلا تذكرته
            if (role == "User" && response.CreatedById != userId)
                return StatusCode(403, new { message = "You do not own this ticket" });

            return Ok(response);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, UpdateTicketaCommands obj)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            if (id != obj.Id)
                return BadRequest(new { message = "You cannot modify the ticket ID." });

            var ticket = await mediat.Send(new GetTicketByIdQuery(id));
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            if (role == "User" && ticket.CreatedById != userId)
                return StatusCode(403, new { message = "You do not own this ticket" });

            var response = await mediat.Send(obj);
            return Ok(response);
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddTicket(CreateTicketsCommand obj)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // تأكد إن الـ User لا يغير الـ CreatedById
            obj.CreatedById = userId;

            try
            {
                var response = await mediat.Send(obj);

                return Ok(new
                {
                    message = "Ticket created successfully",
                    ticketId = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to create ticket", detail = ex.Message });
            }
        }





        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTicket(int id, ITicketReposatory repo)
        {
            var ticket = await repo.GetById(id);

            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            // Read JWT correctly
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Only Admin or Owner
            if (role != "Admin" && ticket.CreatedById != userId)
            {
                return StatusCode(403, new
                {
                    message = "You do not have permission to delete this ticket"
                });
            }

            await repo.Delete(ticket.Id);
            await repo.Save();

            return Ok(new
            {
                message = "Ticket deleted successfully"
            });
        }

    }
}
