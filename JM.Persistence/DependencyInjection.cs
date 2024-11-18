
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using JM.Infrastructure.Models;
using JM.Persistence;
using JM.Application.Common.Generic;

namespace JM.JM.Persistence
{
    public static class DependencyInjection
    {
        public static void AddJMPersistence(this IServiceCollection services,AppSettings appSettings)
        {
            if (appSettings.DbServer == DbServer.MariaDB.ToString())
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(appSettings.ConnectionString, MariaDbServerVersion.LatestSupportedServerVersion,

                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))

                );
            }
            else if (appSettings.DbServer == DbServer.MySQL.ToString())
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(appSettings.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion,

                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))

               );
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   appSettings.ConnectionString,
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))

               );
            }
            services.AddScoped<IDbContextCore>(provider => provider.GetService<ApplicationDbContext>());
        }
    }
}
