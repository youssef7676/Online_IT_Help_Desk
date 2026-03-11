using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsAttachments.Commands.AttachmentCommands
{
    public class CreateAttachmentCommands : IRequest<int>
    {
        public int TicketId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public int CreatedById { get; set; }  

    }
}
