using IT_Help_Desk.Domain.Interfaces;
using IT_Help_Desk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Infrastruction.Reposatories
{
    public class UserReposatory : Reposatory<User> , IUserRepository
    {
        public UserReposatory(HelpDeskDbContext context) : base(context)
        {
            
        }

    }
}
