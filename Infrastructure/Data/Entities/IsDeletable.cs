using DomainArchitecture.Infrastructure.Events;

namespace DomainArchitecture.Infrastructure.Data.Entities {
    public interface IsDeletable {
        DateTime? DeletionDate { get; set; }
    }

    public static class IDeletableExtensions {

        public static void Delete<T>(this T entity) where T : Entity, IsDeletable {
            entity.DeletionDate = DateTime.UtcNow;
            entity.Publish(new Deleted<T>(entity));
        }

        public static IQueryable<T> NotDeleted<T>(this IQueryable<T> query) where T : IsDeletable {
            return query.Where(each => each.DeletionDate == null);
        }
    }
}