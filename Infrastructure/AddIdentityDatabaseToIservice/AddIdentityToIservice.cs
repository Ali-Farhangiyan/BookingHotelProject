using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;
using Application.ContextInterfaces;

namespace Infrastructure.AddIdentityDatabaseToIservice
{
    public static class AddIdentityToIservice
    {
        public static IServiceCollection AddMyIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<IdentityDatabaseContext>(op => op.UseSqlServer(configuration["ConnectionStrings:SqlServer"]));

            

            // you must install-package microsoft.aspnetcore.identity in related assembly to use this extention!
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityDatabaseContext>();

            services.AddTransient<IIdentityDatabaseContext, IdentityDatabaseContext>();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 8;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;


            });
            return services;
        }
    }
}
