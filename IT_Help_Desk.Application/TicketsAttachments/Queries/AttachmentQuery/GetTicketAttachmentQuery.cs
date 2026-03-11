using IT_Help_Desk.Application.TicketsAttachments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsAttachments.Queries.AttachmentQuery
{
    public class GetTicketAttachmentQuery : IRequest<List<AttachmentDTO>>
    {

    }
}
