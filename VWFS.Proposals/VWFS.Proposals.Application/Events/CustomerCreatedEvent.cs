using System;
using VWFS.BuildingBlocks.Domain.Enum;


namespace VWFS.Proposals.Application.Events
{
    public class CustomerCreatedEvent
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public CustomerType Type { get; set; }
        public DateTime BirthOrFoundationDate { get; set; }
    }
}
