using DomainArchitecture.Infrastructure.Data.Entities;

namespace DomainArchitecture.Infrastructure.Data {
    public class Repository<T> where T : HasId {
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