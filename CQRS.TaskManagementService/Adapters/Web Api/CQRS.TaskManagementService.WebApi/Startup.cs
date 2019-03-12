using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CQRS.TaskManagementService.WebApi.CompositionRoot;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.Autofac.Extensions;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.EventStores.EventStore.Extensions;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.TaskManagementService.WebApi
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
                builder.AddUserSecrets<Startup>();
            
            Configuration = builder.Build();
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSerilogLogger();
            services.AddSerilogLoggerToEventFlow();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            return ConfigureContainer(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseMiddleware<CommandPublishMiddleware>();
            app.UseMvc();
        }
        
        private IServiceProvider ConfigureContainer(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            
            EventFlowOptions.New
                .UseAutofacContainerBuilder(containerBuilder)
                .Configure(c =>
                {
                    c.ThrowSubscriberExceptions = true;
                    c.IsAsynchronousSubscribersEnabled = true;
                })
                .AddAspNetCoreMetadataProviders()
                .RegisterModules()
                .ConfigureElasticsearch(new Uri(Configuration["ElasticSearchConnectionString"]))
                .UseEventStoreEventStore(new Uri(Configuration["EventStoreConnectionString"]), ConnectionSettings.Create().UseConsoleLogger().EnableVerboseLogging().Build());
            
            containerBuilder.Populate(services);
            containerBuilder.RegisterModules();
            
            ApplicationContainer = containerBuilder.Build();
            
            return new AutofacServiceProvider(ApplicationContainer);
        }
    }
}
