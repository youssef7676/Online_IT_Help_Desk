using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Infrastruction.Reposatories
{
    public class Reposatory<T>(HelpDeskDbContext context) : IReposatory<T> where T : BaseEntity
    {
        public async Task<int> Add(T obj)
        {
            await context.AddAsync(obj);
            return obj.Id;
        }


        public async Task AddProfile(UserProfile profile)
        {
            await context.UserProfiles.AddAsync(profile);
        }

        public async Task Delete(int Id)
        {
            T obj = await GetById(Id);
            obj.IsDeleted = true;       //  soft delete
        }

        public async Task<User?> DeleteByIdWithProfile(int id)
        {
            return await context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }


        public async Task<List<T>> GetAll(string Include = "")
        {
            if (Include != "")
            {
                return await context.Set<T>().Include(Include).Where(o => o.IsDeleted == false).ToListAsync();
            }
            else
            {
                return await context.Set<T>().Where(o => o.IsDeleted == false).ToListAsync();

            }
        }



        public async Task<List<UserProfile>> GetAllActiveProfilesAsync()
        {
            return await context.UserProfiles
                .Include(p => p.User) 
                .Where(p => !p.IsDeleted && !p.User.IsDeleted)
                .ToListAsync();
        }




        public async Task<List<T>> GetAllUserWithProfile(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>().Where(o => EF.Property<bool>(o, "IsDeleted") == false);

            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }


        public async Task<T> GetById(int Id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(o => o.IsDeleted == false && o.Id == Id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }


        public async Task<User?> GetByEmailWithProfile(string email)
        {
            return await context.Users
                .Include(u => u.Profile)
                        .FirstOrDefaultAsync(u =>
                        u.Email == email &&
                        u.IsDeleted == false);
        }

        public async Task<UserProfile?> GetByUserId(int userId)
        {
            return await context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted);
        }


        public async Task<User?> GetByIdWithProfile(int id)
        {
            return await context.Users
                .Include(u => u.Profile) 
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }



        public async Task<List<Ticket>> GetByUser(int userId)
        {
            return await context.Tickets
                .Where(t => t.CreatedById == userId && !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetByAssignedIT(int itUserId)
        {
            return await context.Tickets
                .Where(t => t.AssignedToId == itUserId && !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<TicketComment>> GetByTicket(int ticketId)
        {
            return await context.TicketComments
                .Where(c => c.TicketId == ticketId && !c.IsDeleted)
                .OrderBy(c => c.CreatedAt)   // علشان التعليقات تبقى بالترتيب
                .ToListAsync();
        }

        public async Task<List<TicketAttachment>> GetByTickets(int ticketId)
        {
            return await context.TicketAttachments
                .Where(a => a.TicketId == ticketId && !a.IsDeleted)
                .ToListAsync();
        }


        public async Task<User?> GetByRefreshToken(string refreshToken)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }


        public async Task<int> Save()
        {
            int num = await context.SaveChangesAsync();
            return num;
        }

        public void Update(T obj)
        {
            context.Update(obj);
            context.SaveChangesAsync();
        }
    }
}
