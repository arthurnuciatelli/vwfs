using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VWFS.Proposals.Domain;

namespace VWFS.Proposals.Infrastructure.Persistence;

public class ProposalsDbContext : DbContext
{
    public ProposalsDbContext(DbContextOptions<ProposalsDbContext> options) : base(options) { }

    public DbSet<Proposal> Proposals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
