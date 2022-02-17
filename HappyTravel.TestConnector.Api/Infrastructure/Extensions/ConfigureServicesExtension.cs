using System.Reflection;
using HappyTravel.BaseConnector.Api.Infrastructure.Environment;
using HappyTravel.BaseConnector.Api.Infrastructure.Extensions;
using HappyTravel.BaseConnector.Api.Services.Accommodations;
using HappyTravel.BaseConnector.Api.Services.Availabilities.AccommodationAvailabilities;
using HappyTravel.BaseConnector.Api.Services.Availabilities.Cancellations;
using HappyTravel.BaseConnector.Api.Services.Availabilities.RoomContractSetAvailabilities;
using HappyTravel.BaseConnector.Api.Services.Availabilities.WideAvailabilities;
using HappyTravel.BaseConnector.Api.Services.Bookings;
using HappyTravel.BaseConnector.Api.Services.Locations;
using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.TestConnector.Api.Infrastructure.Options;
using HappyTravel.TestConnector.Api.Services.Connector;
using HappyTravel.TestConnector.Api.Services.Supplier;
using HappyTravel.VaultClient;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HappyTravel.TestConnector.Api.Infrastructure.Extensions;

public static class ConfigureServicesExtension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var serializationSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None
        };

        JsonConvert.DefaultSettings = () => serializationSettings;

        using var vaultClient = new VaultClient.VaultClient(new VaultOptions
        {
            BaseUrl = new Uri(builder.Configuration[builder.Configuration["Vault:Endpoint"]]),
            Engine = builder.Configuration["Vault:Engine"],
            Role = builder.Configuration["Vault:Role"]
        });

        vaultClient.Login(EnvironmentVariableHelper.Get("Vault:Token", builder.Configuration), LoginMethods.Token)?.GetAwaiter().GetResult();

        builder.Services.AddBaseConnectorServices(builder.Configuration, builder.Environment, vaultClient, Constants.ConnectorName);

        builder.Services.AddTransient<IWideAvailabilitySearchService, WideAvailabilitySearchService>();
        builder.Services.AddTransient<IAccommodationAvailabilityService, AccommodationAvailabilityService>();
        builder.Services.AddTransient<IRoomContractSetAvailabilityService, RoomContractSetAvailabilityService>();
        builder.Services.AddTransient<IBookingService, BookingService>();
        builder.Services.AddTransient<IAccommodationService, AccommodationService>();
        builder.Services.AddTransient<ILocationService, LocationService>();
        builder.Services.AddTransient<IDeadlineService, DeadlineService>();
        builder.Services.AddTransient<ISupplierService, SupplierService>();
        builder.Services.AddTransient<IWideResultStorage, WideResultStorage>();

        builder.Services.AddHealthChecks();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1.0", new OpenApiInfo { Title = "HappyTravel.com TestConnector API", Version = "v1.0" });

            var xmlCommentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFileName);

            options.IncludeXmlComments(xmlCommentsFilePath);
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddSwaggerGenNewtonsoftSupport();

        var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "accommodations.json"));
        builder.Services.Configure<AccommodationStorage>(o =>
        {
            o.Accommodations = JsonConvert.DeserializeObject<List<MultilingualAccommodation>>(json) ?? new(0);
        });

        builder.Services.Configure<Dictionary<string, Models.GenerationOptions>>(builder.Configuration.GetSection("GenerationOptions"));
    }
}