


using JM.Infrastructure.Base;
using JM.Infrastructure.Common;
using JM.Infrastructure.ExceptionHandler;
using JM.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JM.Middleware.Http;
using JM.Middleware.Middleware;

using JM.Application.Repositories.R_Common;
using JM.Application.Interfaces.I_Common;
using JM.Application.Common.Generic;


namespace JM.JM.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJMService(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IConnectionFactory, ConnectionFactory>(s => new ConnectionFactory(appSettings));
            services.AddSingleton<ILogger, Logger<ErrorDetails>>();
            services.AddScoped<IUnitOfWorkJM, UnitOfWorkJM>();
            services.AddScoped<IBaseDapperRepository, BaseDapperRepository>();
            services.AddScoped<ICommonLibRepository, CommonLibRepository>();
           
            #region PR
           
            #endregion

            services.AddScoped<IUserIdentityService, UserIdentityService>();
            return services;
        }
    }
}
