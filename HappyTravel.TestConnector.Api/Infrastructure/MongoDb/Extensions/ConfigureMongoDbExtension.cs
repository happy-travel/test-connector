using HappyTravel.BaseConnector.Api.Infrastructure.Environment;
using HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Interfaces;
using HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Options;
using HappyTravel.TestConnector.Api.Services.Supplier;

namespace HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Extensions;

public static class ConfigureMongoDbExtension
{
    public static void ConfigureMongoDb(this WebApplicationBuilder builder, VaultClient.VaultClient vaultClient)
    {
        if (builder.Environment.IsLocal())
        {
            builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB:Options"));
        }
        else
        {
            var mongodbOptions = vaultClient.Get(builder.Configuration["MongoDB:Options"]).GetAwaiter().GetResult();
            builder.Services.Configure<MongoDbOptions>(o =>
            {
                o.ConnectionString = mongodbOptions["connectionString"];
                o.DatabaseName = mongodbOptions["database"];
            });
        }

        builder.Services.AddSingleton<IMongoDbClient, MongoDbClient>();
    }
}