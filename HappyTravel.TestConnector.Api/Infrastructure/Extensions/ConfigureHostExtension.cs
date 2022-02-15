using System.Diagnostics;
using HappyTravel.BaseConnector.Api.Infrastructure.Extensions;

namespace HappyTravel.TestConnector.Api.Infrastructure.Extensions;

public static class ConfigureHostExtension
{
    public static void ConfigureHost(this WebApplicationBuilder builder)
    {
        builder.WebHost.UseKestrel(options =>
        {
            options.ConfigureBaseConnector();
        });
        
        builder.WebHost.UseDefaultServiceProvider(s =>
        {
            s.ValidateScopes = true;
            s.ValidateOnBuild = true;
        });
                
        builder.WebHost.UseSentry(options =>
        {
            options.Dsn = Environment.GetEnvironmentVariable("HTDC_HOTELBEDS_SENTRY_ENDPOINT");
            options.Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            options.IncludeActivityData = true;
            options.BeforeSend = sentryEvent =>
            {
                if (Activity.Current is null)
                    return sentryEvent;

                foreach (var (key, value) in Activity.Current.Baggage)
                    sentryEvent.SetTag(key, value ?? string.Empty);

                sentryEvent.SetTag("TraceId", Activity.Current.TraceId.ToString());
                sentryEvent.SetTag("SpanId", Activity.Current.SpanId.ToString());

                return sentryEvent;
            };
        });

        builder.Host.ConfigureBaseConnector(Constants.ConnectorName);
    }
}