var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.NKM_UI>("nkm-ui");

builder.Build().Run();
