using AtHome.MigrationService;
using AtHome.WebApi.Database;

var builder = Host.CreateApplicationBuilder(args);
builder.AddNpgsqlDbContext<ApplicationDbContext>("athome");
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
await host.RunAsync();
