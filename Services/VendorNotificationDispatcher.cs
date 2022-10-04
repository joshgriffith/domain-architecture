using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Infrastructure.Events;
using DomainArchitecture.Infrastructure.External;

namespace DomainArchitecture.Services {
    public class VendorNotificationDispatcher : IsObserver<Created<Order>> {
        private readonly VendorApiClient _client;

        public VendorNotificationDispatcher(VendorApiClient client) {
            _client = client;
        }

        public void Handle(Created<Order> e) {
            if (e.Entity.Product.IsFromExternalVendor) {
                var quantityAvailable = _client.NotifyPurchase(e.Entity.Product.Id);
                e.Entity.Product.UpdateAvailability(quantityAvailable);
            }
        }
    }
}