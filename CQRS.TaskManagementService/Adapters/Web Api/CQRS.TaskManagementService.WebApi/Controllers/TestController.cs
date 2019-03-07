using System.Threading;
using System.Threading.Tasks;
using CQRS.TaskManagementService.TaskManagement.Commands.SetName;
using CQRS.TaskManagementService.TaskManagement.Domain;
using CQRS.TaskManagementService.TaskManagement.ReadModels;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.TaskManagementService.WebApi.Controllers
{
    public class TestController : ApiControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        
        public TestController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }
        
        [HttpGet, Route("test-in-memory")]
        public async Task<IActionResult> TestInMemory()
        {
            var exampleId = BoardId.New;
            
            await _commandBus.PublishAsync(new SetName(exampleId, "TestName"), CancellationToken.None)
                .ConfigureAwait(false);

            var exampleReadModel = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<BoardReadModel>(exampleId), CancellationToken.None)
                .ConfigureAwait(false);

            return Ok(exampleReadModel);
        }
    }
}