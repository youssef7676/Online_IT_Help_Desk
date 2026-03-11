using IT_Help_Desk.Application.Tickets.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Tickets.Commands.UpdateTicketCommands
{
    public class UpdateTicketaCommands : IRequest<TicketDTO>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
