using CQRS.TaskManagementService.WebApi.Logging;
using EventFlow.Logs;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.TaskManagementService.WebApi
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSerilogLogger(this IServiceCollection services)
        {
            services.AddSingleton(Serilog.Log.Logger);
            return services;
        }
        
        public static IServiceCollection AddSerilogLoggerToEventFlow(this IServiceCollection services)
        {
            services.AddSingleton<ILog>(new EventFlowSerilogLogger(Serilog.Log.Logger));
            return services;
        }
    }
}