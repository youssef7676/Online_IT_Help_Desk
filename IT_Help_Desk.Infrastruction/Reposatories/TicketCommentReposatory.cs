using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Infrastruction.Reposatories
{
    public class TicketCommentReposatory : Reposatory<TicketComment> , ITicketCommentReposatory
    {
        public TicketCommentReposatory(HelpDeskDbContext context) : base(context) 
        {
            
        }
    }
}
