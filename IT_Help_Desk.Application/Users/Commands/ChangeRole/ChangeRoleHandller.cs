using AutoMapper;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.ChangeRole
{
    public class ChangeRoleHandller(IUserRepository userrepo, IMapper mapper)
        : IRequestHandler<ChangeRoleCommand, bool>
    {
        public async Task<bool> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await userrepo.GetById(request.Id);

            if (user == null)
                throw new Exception("User not found");

            user.Role = request.Role;

            userrepo.Update(user);
            //await userrepo.Save();

            return true;
        }
    }
}
