using IT_Help_Desk.Application.TicketsComments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.Queries.GetById
{
    public class GetCommentsByIdQuery : IRequest<CommentsDTO>
    {
        public int Id { get; set; }

        public GetCommentsByIdQuery(int id)
        {

             Id = id;
            
        }
    }
}
