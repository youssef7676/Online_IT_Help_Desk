using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.TicketsAttachments.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsAttachments.Queries.AttachmentQuery
{
    public class GetTicketAttachmentHandller(ITicketAttachmentReposatory attchrepo, IMapper mapper/*, IHttpContextAccessor httpContext*/)
        : IRequestHandler<GetTicketAttachmentQuery, List<AttachmentDTO>>
    {
        public async Task<List<AttachmentDTO>> Handle(GetTicketAttachmentQuery request, CancellationToken cancellationToken)
        {
            List<TicketAttachment> tickattach = await attchrepo.GetAll();
            List<AttachmentDTO> attchdto = mapper.Map<List<AttachmentDTO>>(tickattach);

            //var http = httpContext.HttpContext.Request;
            //var baseUrl = $"{http.Scheme}://{http.Host}";

            //foreach (var item in attchdto)
            //{
            //    item.FileUrl = baseUrl + item.FileUrl;
            //}

            return attchdto;
        }
    }
}
