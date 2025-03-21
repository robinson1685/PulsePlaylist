var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.PulsePlaylist_Api>("apiservice");

builder.AddProject<Projects.PulsePlaylist_WebApp>("blazorweb")
     .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
