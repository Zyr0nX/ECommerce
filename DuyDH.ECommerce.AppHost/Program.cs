var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DuyDH_ECommerce_User_API>("duydh-ecommerce-user-api");

builder.Build().Run();
