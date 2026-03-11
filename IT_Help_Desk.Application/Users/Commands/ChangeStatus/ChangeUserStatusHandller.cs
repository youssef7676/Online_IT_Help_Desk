using AutoMapper;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.ChangeStatus
{
    public class ChangeUserStatusHandller(IUserRepository repo, IMapper mapper)
        : IRequestHandler<ChangeUserStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await repo.GetById(request.Id);
            if (user == null)
                throw new Exception("User not found");

            user.IsActive = request.IsActive;

            repo.Update(user);
            //await repo.Save();

            return true;
        }
    }
}
