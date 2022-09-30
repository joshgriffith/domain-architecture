using DomainArchitecture.Infrastructure.Data.Entities;

namespace DomainArchitecture.Infrastructure.Data {

    public interface IDatabase {
        IQueryable<T> Get<T>();
        void Add(object entity);
        void Delete(object entity);
        void SaveChanges();
        List<Entity> GetChangeset();
    }

    public class InMemoryDatabaseContext : IDatabase {
        private readonly Dictionary<Type, List<object>> _data = new();

        public List<Entity> GetChangeset() {
            return new List<Entity>();
        }

        public IQueryable<T> Get<T>() {
            if (_data.ContainsKey(typeof(T)))
                return _data[typeof(T)].Cast<T>().AsQueryable();

            return new List<T>().AsQueryable();
        }

        public void Add(object entity) {
            if (!_data.ContainsKey(entity.GetType()))
                _data.Add(entity.GetType(), new List<object>());

            _data[entity.GetType()].Add(entity);
        }

        public void Delete(object entity) {
            if (!_data.ContainsKey(entity.GetType()))
                _data.Add(entity.GetType(), new List<object>());

            _data[entity.GetType()].Remove(entity);
        }

        public void SaveChanges() {
        }
    }
}