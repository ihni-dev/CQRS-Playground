using System.Threading;
using System.Threading.Tasks;
using CQRS.TaskManagementService.TaskManagement.Domain;
using EventFlow.Commands;

namespace CQRS.TaskManagementService.TaskManagement.Commands.SetName
{
    public class SetNameHandler : CommandHandler<Board, BoardId, SetName>
    {
        public override Task ExecuteAsync(Board aggregate, SetName command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            return Task.FromResult(0);
        }
    }
}