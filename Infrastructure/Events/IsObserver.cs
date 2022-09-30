using System;

namespace DomainArchitecture.Infrastructure.Events {
    public interface IsObserver {
    }

    public interface IsObserver<in T> : IsObserver {
        void Handle(T domainEvent);
    }
}