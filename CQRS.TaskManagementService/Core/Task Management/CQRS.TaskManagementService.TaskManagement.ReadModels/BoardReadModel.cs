using CQRS.TaskManagementService.TaskManagement.Domain;
using CQRS.TaskManagementService.TaskManagement.Domain.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace CQRS.TaskManagementService.TaskManagement.ReadModels
{
    public class BoardReadModel : IReadModel, IAmReadModelFor<Board, BoardId, BoardNameChanged>,
        IAmReadModelFor<Board, BoardId, BoardCreated>
    {
        public string Id { get; set; }
        public string BoardName { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<Board, BoardId, BoardCreated> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            BoardName = domainEvent.AggregateEvent.BoardName;
        }

        public void Apply(IReadModelContext context, IDomainEvent<Board, BoardId, BoardNameChanged> domainEvent)
        {
            BoardName = domainEvent.AggregateEvent.BoardName;
        }
    }
}