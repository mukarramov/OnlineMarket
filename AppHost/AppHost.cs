var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApi>("api");

builder.Build().Run();
