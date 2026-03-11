using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.Commands.CreateTicketCommentsCommands
{
    public class CreateCommentsCommands :IRequest<int>
    {
        public int TicketId { get; set; }
        public string Comment { get; set; }
        public int CreatedById { get; set; }
    }
}
