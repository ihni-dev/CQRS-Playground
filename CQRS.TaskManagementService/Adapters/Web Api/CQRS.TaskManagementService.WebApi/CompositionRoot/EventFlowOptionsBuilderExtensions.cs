using CQRS.TaskManagementService.WebApi.CompositionRoot.EventFlowModules;
using EventFlow;

namespace CQRS.TaskManagementService.WebApi.CompositionRoot
{
    internal static class EventFlowOptionsBuilderExtensions
    {
        internal static void RegisterModules(this IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.RegisterModule(new TaskManagementEventFlowModule());
        }
    }
}