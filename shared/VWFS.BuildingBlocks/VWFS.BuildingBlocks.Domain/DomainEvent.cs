using System;

namespace VWFS.BuildingBlocks.Domain
{
    public abstract record DomainEvent(Guid Id, DateTime OccurredOn);
}
