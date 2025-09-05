using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VWFS.Proposals.Domain;

namespace VWFS.Proposals.Infrastructure.Configurations
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Vehicle).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.DownPayment).IsRequired();
            builder.Property(p => p.Installments).IsRequired();
            builder.Property(p => p.MonthlyInterest).IsRequired();
        }
    }
}
