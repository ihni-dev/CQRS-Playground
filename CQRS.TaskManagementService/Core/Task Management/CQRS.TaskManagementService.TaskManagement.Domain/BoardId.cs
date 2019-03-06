using EventFlow.Core;

namespace CQRS.TaskManagementService.TaskManagement.Domain
{
    public class BoardId : Identity<BoardId>
    {
        public BoardId(string value) : base(value) { }
    }
}