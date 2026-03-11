using AutoMapper;
using IT_Help_Desk.Application.Users.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.UsersRigster
{
    public class CreateRigsterHandller(IUserRepository userrepo, IPassword passrepo, IMapper mapper)
        : IRequestHandler<CreateRigsterCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(CreateRigsterCommand request, CancellationToken cancellationToken)
        {
            //  تحقق لو الإيميل موجود بالفعل
            var existing = await userrepo.GetByEmail(request.Data.Email);
            if (existing != null)
                throw new Exception("Email already exists");

            //  حضر User جديد
            var user = new User
            {
                FullName = request.Data.FullName,
                Email = request.Data.Email,
                PasswordHash = passrepo.Hash(request.Data.Password),
                Role = "User"
            };

            await userrepo.Add(user);
            await userrepo.Save();

            return mapper.Map<UserDTO>(user);
        }
    }
}

