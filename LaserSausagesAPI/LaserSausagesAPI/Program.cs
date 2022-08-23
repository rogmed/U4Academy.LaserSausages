using LaserSausagesAPI;
using LaserSausagesAPI.Controllers;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string? key = builder.Configuration["CS"];

if (key == null || string.IsNullOrEmpty(key))
    key = Environment.GetEnvironmentVariable("AZURE_STORAGETABLE_CONNECTIONSTRING");

builder.Services.AddHealthChecks().AddCheck<HealthCheck>("HealthCheck");

builder.Services.AddSingleton<IDBConnector>(new DBConnector(key!));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/api/healthcheck");

app.Run();