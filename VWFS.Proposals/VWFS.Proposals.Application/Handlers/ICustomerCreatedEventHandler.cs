using VWFS.Proposals.Application.Events;

namespace VWFS.Proposals.Application.Handlers
{
    public interface ICustomerCreatedEventHandler
    {
        void Handle(CustomerCreatedEvent customerEvent);
    }
}
