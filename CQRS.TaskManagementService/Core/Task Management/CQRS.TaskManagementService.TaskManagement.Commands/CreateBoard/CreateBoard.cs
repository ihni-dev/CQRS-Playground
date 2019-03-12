using CQRS.TaskManagementService.TaskManagement.Domain;
using EventFlow.Commands;

namespace CQRS.TaskManagementService.TaskManagement.Commands.CreateBoard
{
    public class CreateBoard: Command<Board, BoardId>
    {
        public CreateBoard(BoardId id, string boardName) : base(id)
        {
            Id = id;
            BoardName = boardName;
        }

        public BoardId Id { get; }
        public string BoardName { get; }
    }
}