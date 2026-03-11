using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.TicketsAttachments.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsAttachments.Queries.AttchmentQueryById
{
    public class GetAttachmentByIdHandller(ITicketAttachmentReposatory attacrepo, IMapper mapper)
        : IRequestHandler<GetAttachmentByIdQuery, AttachmentDTO>
    {
        public async Task<AttachmentDTO> Handle(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
        {
            TicketAttachment tickattac = await attacrepo.GetById(request.Id);

            if (tickattac == null)
                return null;

            return mapper.Map<AttachmentDTO>(tickattac);
        }
    }
}
