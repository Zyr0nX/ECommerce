var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("redis");
var sql = builder.AddPostgres("postgres");

var userdb = sql.AddDatabase("userDb");

builder.AddProject<Projects.DuyDH_ECommerce_User_API>("duydh-ecommerce-user-api")
    .WithReference(cache)
    .WithReference(userdb);

builder.AddProject<Projects.DuyDH_ECommerce_User_Migration>("duydh-ecommerce-user-migration")
    .WithReference(userdb);

builder.Build().Run();
