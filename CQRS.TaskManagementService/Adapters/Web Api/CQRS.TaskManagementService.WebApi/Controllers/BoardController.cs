using System.Threading;
using System.Threading.Tasks;
using CQRS.TaskManagementService.TaskManagement.Commands.CreateBoard;
using CQRS.TaskManagementService.TaskManagement.Commands.SetName;
using CQRS.TaskManagementService.TaskManagement.Domain;
using CQRS.TaskManagementService.TaskManagement.ReadModels;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CQRS.TaskManagementService.WebApi.Controllers
{
    public class BoardController : ApiControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ILogger<BoardController> _logger;
        private readonly IQueryProcessor _queryProcessor;

        public BoardController(ICommandBus commandBus, IQueryProcessor queryProcessor, ILogger<BoardController> logger)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _logger = logger;
        }

        [HttpGet]
        [Route("{boardIdValue}")]
        public async Task<IActionResult> GetAsync(string boardIdValue)
        {
            _logger.LogCritical("**************************GET OPERATION**************************");

            var boardId = BoardId.With(boardIdValue);
            var board = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<BoardReadModel>(boardId),
                default(CancellationToken));

            return Ok(board);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] string boardName)
        {
            var boardId = BoardId.New;
            await _commandBus.PublishAsync(new CreateBoard(boardId, boardName), CancellationToken.None);

            return CreatedAtAction(nameof(GetAsync), new {boardIdValue = boardId.ToString()},
                new {id = boardId.Value, name = boardName});
        }

        [HttpPatch]
        [Route("{boardIdValue}")]
        public async Task<IActionResult> UpdateNameAsync(string boardIdValue, [FromBody] string newName)
        {
            var boardId = BoardId.With(boardIdValue);
            await _commandBus.PublishAsync(new ChangeBoardName(boardId, newName), default(CancellationToken));

            return NoContent();
        }
    }
}