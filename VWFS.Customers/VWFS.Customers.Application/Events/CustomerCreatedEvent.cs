using System;
using VWFS.Customers.Domain.Entities;

namespace VWFS.Customers.Application.Events
{
    public class CustomerCreatedEvent
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty; // CPF ou CNPJ
        public CustomerType Type { get; set; }
        public DateTime BirthOrFoundationDate { get; set; }
    }
}
