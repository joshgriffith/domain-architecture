using DomainArchitecture.Infrastructure.Data;
using DomainArchitecture.Infrastructure.Data.Entities;
using DomainArchitecture.Infrastructure.Events;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomainArchitecture.Infrastructure.ActionFilters {

    /// <summary>
    /// Responsible for dispatching events and committing changes to the database at the end of an action
    /// </summary>
    public class CommitChangesActionFilter : IActionFilter {

        private readonly IDatabase _database;
        private readonly EventRouter _router;

        public CommitChangesActionFilter(IDatabase database, EventRouter router) {
            _database = database;
            _router = router;
        }

        public void OnActionExecuting(ActionExecutingContext context) {
        }

        public void OnActionExecuted(ActionExecutedContext context) {

            foreach (var entity in _database.GetChangeset()) {

                // If the entity is new, publish a Created event (using reflection since it's a generic event)
                if (entity.IsNew())
                    GetType().GetMethod(nameof(PublishCreatedEvent)).MakeGenericMethod(entity.GetType()).Invoke(entity, null);

                foreach (var e in entity.GetEvents()) {
                    
                    // If the entity has a Deleted event, mark it as deleted in the database
                    if (e.GetType().IsInstanceOfType(typeof(Deleted<>)))
                        _database.Delete(e);

                    // Publish each event to the router, which routes them to services. This could instead be called after the database.SaveChanges
                    _router.Publish(e);
                }
            }

            _database.SaveChanges();
        }

        private void PublishCreatedEvent<T>(T entity) where T : Entity {
            entity.Publish(new Created<T>(entity));
        }
    }
}