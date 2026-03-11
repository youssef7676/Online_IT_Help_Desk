using IT_Help_Desk.Domain.Interfaces;
using IT_Help_Desk.Infrastruction.Reposatories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Infrastruction.Extentions
{
    public static class ExtentionsMethods
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager cfgmanager)
        {
            services.AddDbContext<HelpDeskDbContext>(option =>
            {
                option.UseSqlServer(cfgmanager.GetConnectionString("CS"));
            });

            services.AddScoped<IUserRepository ,  UserReposatory>();
            services.AddScoped<IUserProfileReposatory , UserProfileReposatory>();
            services.AddScoped<ITicketReposatory, TicketReposatory>();
            services.AddScoped<ITicketCommentReposatory, TicketCommentReposatory>();
            services.AddScoped<ITicketAttachmentReposatory, TicketAttachmentReposatory>();
            services.AddScoped<IPassword, PasswordReposatory>();
            services.AddScoped<IJwtService, JwtReposatory>();



        }

    }
}
