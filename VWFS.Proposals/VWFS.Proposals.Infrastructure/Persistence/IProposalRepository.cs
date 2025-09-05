using VWFS.Proposals.Domain;

namespace VWFS.Proposals.Infrastructure.Persistence;

public interface IProposalRepository
{
    Task AddAsync(Proposal proposal);
}

