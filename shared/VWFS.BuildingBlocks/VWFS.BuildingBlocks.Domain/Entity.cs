using System;

namespace VWFS.BuildingBlocks.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    }
}
