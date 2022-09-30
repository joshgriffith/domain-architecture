using System;

namespace DomainArchitecture.Domain.Orders.Events {
    public class OrderShipped {
        public Order Order { get; set; }

        public OrderShipped(Order order) {
            Order = order;
        }
    }
}