
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using JM.Infrastructure.Models;

namespace JM.JM.Persistence
{
    public static class DependencyInjection
    {
        public static void AddHRPersistence(this IServiceCollection services,AppSettings appSettings)
        {
            //if (appSettings.DbServer==DbServer.MariaDB.ToString())
            //{
            //    services.AddDbContext<HRDbContext>(options =>
            //    options.UseMySql(appSettings.ConnectionString,MariaDbServerVersion.LatestSupportedServerVersion,b=> b.MigrationsAssembly(typeof(HRDbContext).Assembly.FullName)));

            //}
            //else if (appSettings.DbServer==DbServer.MySQL.ToString())
            //{
            //    services.AddDbContext<HRDbContext>(options =>
            //    options.UseMySql(appSettings.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion, b => b.MigrationsAssembly(typeof(HRDbContext).Assembly.FullName)));

            //}

            //else
            //{
            //    services.AddDbContext<HRDbContext>(options => options.UseSqlServer(appSettings.ConnectionString, b => b.MigrationsAssembly(typeof(HRDbContext).Assembly.FullName)));
            //}

            //services.AddScoped<IHRDbContext>(provider => provider.GetService<HRDbContext>());
            //services.AddScoped<IUserIdentityService, UserIdentityService>();
            //services.AddScoped<IDbContextCore>(provider => provider.GetService<ApplicationDbContext>());

        }
    }
}
