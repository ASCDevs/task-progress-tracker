using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Infrastructure.ConnectionsServices;
using TaskTracker.Infrastructure.Interfaces;
using TaskTracker.Infrastructure.Persistence.Context;

namespace TaskTracker.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SQLServerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConn")));


            return services;
        }

        public static IServiceCollection AddInfrastructureConnections(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionTaskServer, ConnectionTaskServer>();

            return services;
        }
    }
}
