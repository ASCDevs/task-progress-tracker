using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Domain.Repositories;
using TaskTracker.Infrastructure.ConnectionsServices;
using TaskTracker.Infrastructure.Interfaces;
using TaskTracker.Infrastructure.Persistence.Context;
using TaskTracker.Infrastructure.Persistence.Repositories;

namespace TaskTracker.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SQLServerContext>();
            services.AddSingleton<ITarefaRepo, TarefaRepo>();

            return services;
        }

        public static IServiceCollection AddInfrastructureConnections(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionTaskServer, ConnectionTaskServer>();

            return services;
        }
    }
}
