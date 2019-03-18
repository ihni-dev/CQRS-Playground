using Autofac;

namespace CQRS.TaskManagementService.WebApi.CompositionRoot
{
    internal static class AutofacContainerBuilderExtensions
    {
        internal static ContainerBuilder RegisterModules(this ContainerBuilder builder)
        {
            return builder;
        }
    }
}