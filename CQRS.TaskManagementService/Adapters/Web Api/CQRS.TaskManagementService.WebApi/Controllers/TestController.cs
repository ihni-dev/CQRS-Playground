using System.Threading;
using System.Threading.Tasks;
using CQRS.TaskManagementService.TaskManagement.Commands.SetName;
using CQRS.TaskManagementService.TaskManagement.Domain;
using CQRS.TaskManagementService.TaskManagement.Domain.Events;
using CQRS.TaskManagementService.TaskManagement.ReadModels;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.TaskManagementService.WebApi.Controllers
{
    public class TestController : ApiControllerBase
    {
        [HttpGet, Route("test-in-memory")]
        public async Task<IActionResult> TestInMemory()
        {
            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(NameSet))
                .AddCommands(typeof(SetName))
                .AddCommandHandlers(typeof(SetNameHandler))
                .UseInMemoryReadStoreFor<BoardReadModel>()
                .CreateResolver())
            {
                var exampleId = BoardId.New;

                var commandBus = resolver.Resolve<ICommandBus>();
                await commandBus.PublishAsync(
                        new SetName(exampleId, "TestName"), CancellationToken.None)
                    .ConfigureAwait(false);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                var exampleReadModel = await queryProcessor.ProcessAsync(
                        new ReadModelByIdQuery<BoardReadModel>(exampleId), CancellationToken.None)
                    .ConfigureAwait(false);

                return Ok(exampleReadModel);
            }
        }
    }
}