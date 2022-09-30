using DomainArchitecture.Infrastructure.Data.Entities;

namespace DomainArchitecture.Infrastructure.Data {

    /// <summary>
    /// Abstraction so that underlying data provider can be changed
    /// </summary>
    public sealed class Repository<T> where T : Entity {
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