using DomainArchitecture.Infrastructure.Data.Entities;
using DomainArchitecture.Infrastructure.Events;

namespace DomainArchitecture.Infrastructure.Data {
    public class Repository<T> where T : Entity {
        private readonly IDatabase _database;

        public Repository(IDatabase database) {
            _database = database;
        }

        public IQueryable<T> Get() {
            return _database.Get<T>();
        }

        public T Add(T entity) {
            _database.Add(entity);
            return entity;
        }
    }
}