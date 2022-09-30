using DomainArchitecture.Infrastructure.Data;
using DomainArchitecture.Infrastructure.Events;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomainArchitecture.Infrastructure.ActionFilters
{
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
            _database.SaveChanges();

            foreach (var entity in _database.GetChangeset())
                foreach (var e in entity.GetEvents())
                    _router.Publish(e);
        }
    }
}