using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CQRS.TaskManagementService.WebApi
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSerilogLogger(this IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
            return services;
        }
    }
}