using EventFlow.Aggregates;

namespace CQRS.TaskManagementService.TaskManagement.Domain.Events
{
    public class BoardNameChanged : AggregateEvent<Board, BoardId>
    {
        public BoardNameChanged(string boardName)
        {
            BoardName = boardName;
        }

        public string BoardName { get; }
    }
}