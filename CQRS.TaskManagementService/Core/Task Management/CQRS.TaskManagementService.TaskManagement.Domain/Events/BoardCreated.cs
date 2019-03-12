using EventFlow.Aggregates;

namespace CQRS.TaskManagementService.TaskManagement.Domain.Events
{
    public class BoardCreated : AggregateEvent<Board, BoardId>
    {
        public BoardCreated(string boardName)
        {
            BoardName = boardName;
        }
    
        public string BoardName { get; }
    }
}