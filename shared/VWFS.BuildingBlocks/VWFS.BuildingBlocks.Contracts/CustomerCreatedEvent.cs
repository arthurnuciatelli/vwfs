using System;

namespace VWFS.BuildingBlocks.Contracts;

public record ContractCreatedEvent(Guid Id, Guid CustomerId, string Vehicle, int Parcels);
