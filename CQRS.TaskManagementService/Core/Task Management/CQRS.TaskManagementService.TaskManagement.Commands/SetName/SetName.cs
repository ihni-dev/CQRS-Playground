using CQRS.TaskManagementService.TaskManagement.Domain;
using EventFlow.Commands;

namespace CQRS.TaskManagementService.TaskManagement.Commands.SetName
{
    public class SetName : Command<Board, BoardId>
    {
        public SetName(BoardId aggregateId, string name) : base(aggregateId)
        {
            Name = name;
        }

        public string Name { get; }
    }
}