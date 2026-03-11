using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.DTOs
{
    public class CommentsDTO
    {
        public int TicketId { get; set; }
        public string Comment { get; set; }
    }
}
