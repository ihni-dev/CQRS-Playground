using CQRS.TaskManagementService.TaskManagement.Domain.Events;
using EventFlow.Aggregates;

namespace CQRS.TaskManagementService.TaskManagement.Domain
{
    public class Board : AggregateRoot<Board, BoardId>, IEmit<BoardCreated>, IEmit<BoardNameChanged>
    {
        public Board(BoardId id) : base(id)
        {
        }

        public string BoardName { get; protected set; } = string.Empty;

        public void Apply(BoardCreated aggregateEvent)
        {
            BoardName = aggregateEvent.BoardName;
        }

        public void Apply(BoardNameChanged aggregateEvent)
        {
            BoardName = aggregateEvent.BoardName;
        }

        public void Create(string boardName)
        {
            Emit(new BoardCreated(boardName));
        }

        public void ChangeBoardName(string newBoardName)
        {
            Emit(new BoardNameChanged(newBoardName));
        }
    }
}