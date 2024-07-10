using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin();

var postgresdb = postgres.AddDatabase("athome");

var api = builder.AddProject<Projects.AtHome_WebApi>("athome-webapi")
    .WithReference(postgresdb);

builder.AddProject<Projects.AtHome_Web>("athome-web")
    .WithReference(api);

builder.Build().Run();