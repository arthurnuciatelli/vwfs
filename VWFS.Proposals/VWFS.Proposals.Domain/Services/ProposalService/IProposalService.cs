using System;
using VWFS.BuildingBlocks.Contracts;
using VWFS.Proposals.Domain;

namespace VWFS.Proposals.Domain.Services.ProposalService
{
    public interface IProposalService
    {
        Proposal GerarProposta(CustomerCreatedEvent customer);
    }
}
