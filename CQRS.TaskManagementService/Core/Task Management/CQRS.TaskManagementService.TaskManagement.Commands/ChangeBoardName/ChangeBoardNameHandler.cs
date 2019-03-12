using System.Threading;
using System.Threading.Tasks;
using CQRS.TaskManagementService.TaskManagement.Domain;
using EventFlow.Commands;

namespace CQRS.TaskManagementService.TaskManagement.Commands.SetName
{
    public class ChangeBoardNameHandler : CommandHandler<Board, BoardId, ChangeBoardName>
    {
        public override Task ExecuteAsync(Board aggregate, ChangeBoardName command, CancellationToken cancellationToken)
        {
            aggregate.ChangeBoardName(command.BoardName);
            return Task.FromResult(0);
        }
    }
}