using VWFS.Proposals.Application.Events;
using VWFS.Proposals.Domain;
using VWFS.Proposals.Infrastructure.Persistence;

namespace VWFS.Proposals.Application.Handlers
{
    public class CustomerCreatedEventHandler : ICustomerCreatedEventHandler
    {
        private readonly IProposalRepository _repository;

        public CustomerCreatedEventHandler(IProposalRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CustomerCreatedEvent customerEvent)
        {
            var proposal = new Proposal
            {
                Id = Guid.NewGuid(),
                CustomerId = customerEvent.CustomerId,
                Price = 10000m, // valor inicial padrão
                Year = 2000,
                Vehicle = "Modelo Padrão",
                DownPayment = 2000m,
                Installments = 24,
                MonthlyInterest = 0.01m
            };

            _repository.AddAsync(proposal).Wait();
        }
    }
}
