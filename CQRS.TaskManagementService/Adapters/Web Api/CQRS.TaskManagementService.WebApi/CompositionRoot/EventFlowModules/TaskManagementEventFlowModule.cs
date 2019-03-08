using Autofac;
using CQRS.TaskManagementService.TaskManagement.Commands.SetName;
using CQRS.TaskManagementService.TaskManagement.Domain.Events;
using CQRS.TaskManagementService.TaskManagement.ReadModels;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace CQRS.TaskManagementService.WebApi.CompositionRoot.EventFlowModules
{
    internal class TaskManagementEventFlowModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            RegisterEvents(eventFlowOptions);
            RegisterCommands(eventFlowOptions);
            RegisterCommandHandlers(eventFlowOptions);
            RegisterReadStores(eventFlowOptions);
        }

        private void RegisterEvents(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddEvents(typeof(NameSet));
        }

        private void RegisterCommands(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddCommands(typeof(SetName));
        }
        
        private void RegisterCommandHandlers(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddCommandHandlers(typeof(SetNameHandler));
        }

        private void RegisterReadStores(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.UseInMemoryReadStoreFor<BoardReadModel>();
        }
    }
}