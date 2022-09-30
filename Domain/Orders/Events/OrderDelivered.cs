using System;

namespace DomainArchitecture.Domain.Orders.Events {
    public class OrderDelivered {
        public Order Order { get; set; }

        public OrderDelivered(Order order) {
            Order = order;
        }
    }
}