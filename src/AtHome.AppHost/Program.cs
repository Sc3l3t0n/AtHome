var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloakContainer("keycloak").WithImport("./realms");

var postgres = builder.AddPostgres("postgres").WithPgAdmin();

var postgresdb = postgres.AddDatabase("athome");

builder.AddProject<Projects.AtHome_MigrationService>("athome-migrationservice")
    .WithReference(postgresdb);

var api = builder.AddProject<Projects.AtHome_WebApi>("athome-webapi")
    .WithReference(postgresdb)
    .WithReference(keycloak);

builder.AddProject<Projects.AtHome_Web>("athome-web")
    .WithReference(api);

await builder.Build().RunAsync();