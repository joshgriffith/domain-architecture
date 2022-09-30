using DomainArchitecture.Infrastructure.ActionFilters;
using DomainArchitecture.Infrastructure.Data;
using DomainArchitecture.Infrastructure.Events;
using DomainArchitecture.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.Filters.Add<CommitChangesActionFilter>();
});

builder.Services.AddScoped<IDatabase, InMemoryDatabaseContext>();
builder.Services.AddScoped(typeof(Repository<>));
builder.Services.AddScoped<EmailDispatcher>();
builder.Services.AddScoped<EventRouter>();

var app = builder.Build();
app.MapControllers();
app.Run();