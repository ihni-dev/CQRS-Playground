using EventFlow.Aggregates;

namespace CQRS.TaskManagementService.TaskManagement.Domain.Events
{
    public class NameSet : AggregateEvent<Board, BoardId>
    {
        public NameSet(string name)
        {
            Name = name;
        }
    
        public string Name { get; }
    }
}