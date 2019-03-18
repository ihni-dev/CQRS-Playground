using System;
using Autofac;
using CQRS.TaskManagementService.WebApi.CompositionRoot;
using CQRS.TaskManagementService.WebApi.Logging;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.EventStores.EventStore.Extensions;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CQRS.TaskManagementService.WebApi
{
    public class StartupExtensions
    {
        internal static void ConfigureEventFlow(IConfiguration configuration, ContainerBuilder containerBuilder)
        {
            EventFlowOptions.New
                .UseAutofacContainerBuilder(containerBuilder)
                .Configure(c =>
                {
                    c.ThrowSubscriberExceptions = true;
                    c.IsAsynchronousSubscribersEnabled = true;
                })
                .AddAspNetCoreMetadataProviders()
                .RegisterModules()
                .ConfigureElasticsearch(new Uri(configuration["ElasticSearchConnectionString"]))
                .UseEventStoreEventStore(new Uri(configuration["EventStoreConnectionString"]),
                    ConnectionSettings
                        .Create()
                        .UseCustomLogger(new EventStoreSerilogLogger(Log.Logger))
                        .EnableVerboseLogging()
                        .Build()
                );
        }
    }
}