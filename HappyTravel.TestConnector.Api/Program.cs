using HappyTravel.BaseConnector.Api.Infrastructure.Extensions;
using HappyTravel.Infrastructure.Extensions;
using HappyTravel.TestConnector.Api.Infrastructure;
using HappyTravel.TestConnector.Api.Infrastructure.Extensions;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureInfrastructure(options =>
{
    options.ConsulKey = Constants.ConnectorName;
});
builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureBaseConnector(builder.Configuration);
app.Run();
