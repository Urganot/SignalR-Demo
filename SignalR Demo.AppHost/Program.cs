var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.SignalR_Demo_ApiService>("apiservice");

builder.AddProject<Projects.SignalR_Demo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
