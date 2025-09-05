
using System;

namespace VWFS.BuildingBlocks.Contracts;

public record CustomerCreatedEvent(Guid Id, string Name, string Document, int Type);
