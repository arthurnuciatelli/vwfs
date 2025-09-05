using System;

namespace VWFS.BuildingBlocks.Contracts;

public record PaymentRegisteredEvent(Guid Id, Guid ContractId, int ParcelNumber, bool Late);
