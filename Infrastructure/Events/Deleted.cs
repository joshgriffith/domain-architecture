namespace DomainArchitecture.Infrastructure.Events {
    public class Deleted<T> {
        public T Entity { get; set; }

        public Deleted(T entity) {
            Entity = entity;
        }
    }
}