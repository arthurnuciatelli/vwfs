using System;
using VWFS.Proposals.Domain;

namespace VWFS.Proposals.Infrastructure.Persistence
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly ProposalsDbContext _context;

        public ProposalRepository(ProposalsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Proposal proposal)
        {
            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();
        }
    }
}
