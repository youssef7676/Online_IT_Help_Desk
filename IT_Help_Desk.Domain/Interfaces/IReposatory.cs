using IT_Help_Desk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Domain.Interfaces
{
    public interface IReposatory<T> where T : BaseEntity
    {
        public Task<int> Add(T obj);
        public Task AddProfile(UserProfile profile);
        public void Update(T obj);
        public Task Delete(int Id);
        public Task<User?> DeleteByIdWithProfile(int id);
        public Task<int> Save();
        public Task<User?> GetByEmail(string email);
        public Task<User?> GetByEmailWithProfile(string email);
        public Task<UserProfile?> GetByUserId(int userId);
        public Task<List<Ticket>> GetByUser(int userId);
        public Task<List<Ticket>> GetByAssignedIT(int itUserId);
        public Task<List<TicketComment>> GetByTicket(int ticketId);
        public Task<List<TicketAttachment>> GetByTickets(int ticketId);
        public Task<User?> GetByRefreshToken(string refreshToken);
        public Task<T> GetById(int Id);
        public Task<User?> GetByIdWithProfile(int id);
        public Task<List<T>> GetAll(string Include = "");
        public Task<List<UserProfile>> GetAllActiveProfilesAsync();
        public Task<List<T>> GetAllUserWithProfile(params Expression<Func<T, object>>[] includes);

    }
}
