namespace DomainArchitecture.Infrastructure.Data.Entities {
    public interface HasId {
        int Id { get; set; }
    }

    public static class IEntityExtensions {
        public static T? ById<T>(this IQueryable<T> query, int id) where T : HasId {
            return query.FirstOrDefault(each => each.Id == id);
        }

        public static bool IsNew<T>(this T entity) where T : HasId {
            return entity.Id == 0;
        }
    }
}