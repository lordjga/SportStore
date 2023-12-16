using Basket.Application.Handlers;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
//Redis Settings
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("CacheConnection");
});
builder.Services.AddHealthChecks()
    .AddRedis(
        builder.Configuration.GetConnectionString("CacheConnection"),
        "Redis Health",
        HealthStatus.Degraded);
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Basket.API", Version = "v1" }));

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateShoppingCartHandler>());
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API");
        c.RoutePrefix = "api/swagger";
    });
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => "Hello World!");

app.Run();
