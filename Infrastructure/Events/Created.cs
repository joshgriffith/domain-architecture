namespace DomainArchitecture.Infrastructure.Events {
    public class Created<T> {
        public T Entity { get; set; }

        public Created(T entity) {
            Entity = entity;
        }
    }
}