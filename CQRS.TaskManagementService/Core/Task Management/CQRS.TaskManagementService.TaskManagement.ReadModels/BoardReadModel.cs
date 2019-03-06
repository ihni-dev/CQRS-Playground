using CQRS.TaskManagementService.TaskManagement.Domain;
using CQRS.TaskManagementService.TaskManagement.Domain.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace CQRS.TaskManagementService.TaskManagement.ReadModels
{
    public class BoardReadModel : IReadModel, IAmReadModelFor<Board, BoardId, NameSet>
    {
        public string Name { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<Board, BoardId, NameSet> domainEvent)
        {
            Name = domainEvent.AggregateEvent.Name;
        }
    }
}