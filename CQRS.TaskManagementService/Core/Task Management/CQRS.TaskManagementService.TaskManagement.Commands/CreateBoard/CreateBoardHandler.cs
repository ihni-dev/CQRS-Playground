using System.Threading;
using System.Threading.Tasks;
using CQRS.TaskManagementService.TaskManagement.Domain;
using EventFlow.Commands;

namespace CQRS.TaskManagementService.TaskManagement.Commands.CreateBoard
{
    public class CreateBoardHandler : CommandHandler<Board, BoardId, CreateBoard>
    {
        public override Task ExecuteAsync(Board aggregate, CreateBoard command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.BoardName);
            return Task.FromResult(0);
        }
    }
}