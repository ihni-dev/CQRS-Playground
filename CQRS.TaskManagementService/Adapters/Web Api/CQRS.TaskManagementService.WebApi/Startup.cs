﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CQRS.TaskManagementService.WebApi.CompositionRoot;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.Autofac.Extensions;
using EventFlow.EventStores.Files;
using EventFlow.Extensions;
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
        public IConfigurationRoot Configuration { get; private set; }
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSerilogLogger();
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
                .UseLibLog(LibLogProviders.Serilog)
                .UseFilesEventStore(FilesEventStoreConfiguration.Create("./evt-store"))
                .RegisterModules();
            
            containerBuilder.Populate(services);
            containerBuilder.RegisterModules();
            
            ApplicationContainer = containerBuilder.Build();
            
            return new AutofacServiceProvider(ApplicationContainer);
        }
    }
}
