using Catalog.Application.Handlers.Command;
using Catalog.Core.Core;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration.GetConnectionString("DefaultConnection"),
                "Catalog Mongo Db Health Check",
                HealthStatus.Degraded);

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Catalog.API", Version = "v1" }));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateProductHandler>());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API");
        c.RoutePrefix = "api/swagger";
    });
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
