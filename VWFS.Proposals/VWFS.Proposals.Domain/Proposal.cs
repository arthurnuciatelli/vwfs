using System;

namespace VWFS.Proposals.Domain
{
    public class Proposal
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Vehicle { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public decimal DownPayment { get; set; }
        public int Installments { get; set; }
        public decimal MonthlyInterest { get; set; }

        public Proposal() { }

        public Proposal(Guid customerId, string vehicle, int year, decimal price,
                        decimal downPayment, int installments, decimal monthlyInterest)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Vehicle = vehicle;
            Year = year;
            Price = price;
            DownPayment = downPayment;
            Installments = installments;
            MonthlyInterest = monthlyInterest;
        }
    }
}