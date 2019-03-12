using CQRS.TaskManagementService.TaskManagement.Domain;
using EventFlow.Commands;

namespace CQRS.TaskManagementService.TaskManagement.Commands.SetName
{
    public class ChangeBoardName : Command<Board, BoardId>
    {
        public ChangeBoardName(BoardId aggregateId, string boardName) : base(aggregateId)
        {
            BoardName = boardName;
        }

        public string BoardName { get; }
    }
}