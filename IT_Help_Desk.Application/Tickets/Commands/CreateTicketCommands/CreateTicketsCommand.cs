using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Tickets.Commands.CreateTicketCommands
{
    public class CreateTicketsCommand :IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedById { get; set; }  // Hardcoded دلوقتي
        public int? AssignedToId { get; set; } 
    }
}
