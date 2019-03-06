using CQRS.TaskManagementService.TaskManagement.Domain.Events;
using EventFlow.Aggregates;
using EventFlow.Exceptions;

namespace CQRS.TaskManagementService.TaskManagement.Domain
{
    public class Board : AggregateRoot<Board, BoardId>
    {
        private string _name;
    
        public Board(BoardId id) : base(id) { }
    
        public void SetName(string name)
        {
            if (_name != null)
                throw DomainError.With("Name already set");
    
            Emit(new NameSet(name));
        }
        
        public void Apply(NameSet aggregateEvent)
        {
            _name = aggregateEvent.Name;
        }
    }
}