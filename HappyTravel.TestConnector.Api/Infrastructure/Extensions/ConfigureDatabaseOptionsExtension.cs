using HappyTravel.TestConnector.Data;
using Microsoft.EntityFrameworkCore;

namespace HappyTravel.TestConnector.Api.Infrastructure.Extensions;

public static class ConfigureDatabaseOptionsExtension
{
    public static void ConfigureDatabaseOptions(this WebApplicationBuilder builder, VaultClient.VaultClient vaultClient)
    {
        var databaseOptions = vaultClient.Get(builder.Configuration["Database:Options"]).GetAwaiter().GetResult();
            
        builder.Services.AddDbContextPool<TestConnectorContext>(options =>
        {
            var host = databaseOptions["host"];
            var port = databaseOptions["port"];
            var password = databaseOptions["password"];
            var userId = databaseOptions["userId"];
            var commandTimeout = builder.Configuration.GetValue<int>("Database:CommandTimeout");
            
            var connectionString = builder.Configuration["Database:ConnectionString"];
            options.UseNpgsql(string.Format(connectionString, host, port, userId, password, commandTimeout.ToString()), builder =>
            {
                builder.EnableRetryOnFailure();
            });
            options.UseInternalServiceProvider(null);
            options.EnableSensitiveDataLogging(false);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, 16);
    }
}