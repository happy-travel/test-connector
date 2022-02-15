using HappyTravel.BaseConnector.Api.Infrastructure.Extensions;
using HappyTravel.TestConnector.Api.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureHost();
builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureBaseConnector();
app.Run();
