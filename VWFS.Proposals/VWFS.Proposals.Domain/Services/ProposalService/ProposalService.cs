using System;
using VWFS.BuildingBlocks.Contracts;
using VWFS.Proposals.Domain;

namespace VWFS.Proposals.Domain.Services.ProposalService
{
    public class ProposalService : IProposalService
    {
        public ProposalService()
        {
            
        }
        
        public Proposal GerarProposta(CustomerCreatedEvent customer)
        {
            return new Proposal(
                customer.Id,
                vehicle: "Volkswagen Golf",
                year: 2024,
                price: 100_000m,
                downPayment: 20_000m,
                installments: 48,
                monthlyInterest: 0.015m
            );
        }
    }
}
