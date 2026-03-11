using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using IT_Help_Desk.Application.TicketsComments.DTOs;
using IT_Help_Desk.Application.TicketsAttachments.DTOs;
using IT_Help_Desk.Application.Users.DTOs;
using IT_Help_Desk.Application.UsersProfiles.DTOs;

namespace IT_Help_Desk.Application.Extentions
{
    public static class ExtentionAppClass
    {
        public static void ApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<TicketsProfile>();
            });


            //---------------------------------------------------------------------------------

            var assembly1 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg1 =>
            {
                cfg1.RegisterServicesFromAssembly(assembly1);
            });
            services.AddAutoMapper(cfg1 =>
            {
                cfg1.AddProfile<TicketsProfile>();
            });


            //---------------------------------------------------------------------------------

            var assembly2 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg2 =>
            {
                cfg2.RegisterServicesFromAssembly(assembly2);
            });
            services.AddAutoMapper(cfg2 =>
            {
                cfg2.AddProfile<CommentsProfile>();
            });

            //---------------------------------------------------------------------------------

            var assembly3 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg3 =>
            {
                cfg3.RegisterServicesFromAssembly(assembly3);
            });
            services.AddAutoMapper(cfg3 =>
            {
                cfg3.AddProfile<CommentsProfile>();
            });

            //---------------------------------------------------------------------------------

            var assembly4 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg4 =>
            {
                cfg4.RegisterServicesFromAssembly(assembly4);
            });
            services.AddAutoMapper(cfg4 =>
            {
                cfg4.AddProfile<CommentsProfile>();
            });


            //---------------------------------------------------------------------------------

            var assembly5 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg5 =>
            {
                cfg5.RegisterServicesFromAssembly(assembly5);
            });
            services.AddAutoMapper(cfg5 =>
            {
                cfg5.AddProfile<CommentsProfile>();
            });


            //---------------------------------------------------------------------------------

            var assembly6 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg6 =>
            {
                cfg6.RegisterServicesFromAssembly(assembly6);
            });
            services.AddAutoMapper(cfg6 =>
            {
                cfg6.AddProfile<AttachmentProfile>();
            });


            //---------------------------------------------------------------------------------

            var assembly7 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg7 =>
            {
                cfg7.RegisterServicesFromAssembly(assembly7);
            });
            services.AddAutoMapper(cfg7 =>
            {
                cfg7.AddProfile<AttachmentProfile>();
            });

            //---------------------------------------------------------------------------------

            var assembly8 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg8 =>
            {
                cfg8.RegisterServicesFromAssembly(assembly8);
            });
            services.AddAutoMapper(cfg8 =>
            {
                cfg8.AddProfile<UserProfile>();
            });


            //---------------------------------------------------------------------------------

            var assembly9 = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg9 =>
            {
                cfg9.RegisterServicesFromAssembly(assembly9);
            });
            services.AddAutoMapper(cfg9 =>
            {
                cfg9.AddProfile<UserInfoProfile>();
            });


        }


    }
}
