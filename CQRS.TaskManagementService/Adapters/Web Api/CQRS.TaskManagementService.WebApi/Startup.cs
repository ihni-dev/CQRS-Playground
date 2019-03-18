using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CQRS.TaskManagementService.WebApi.CompositionRoot;
using EventFlow.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.TaskManagementService.WebApi
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IContainer ApplicationContainer { get; set; }
        private IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMiddleware<CommandPublishMiddleware>();
            app.UseMvc();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return ConfigureContainer(services);
        }

        private IServiceProvider ConfigureContainer(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            StartupExtensions.ConfigureEventFlow(Configuration, containerBuilder);
            containerBuilder.Populate(services);
            containerBuilder.RegisterModules();

            ApplicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }
    }
}