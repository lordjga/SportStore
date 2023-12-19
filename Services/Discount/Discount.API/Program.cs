using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Core;
using Discount.Core.Repositories;
using Discount.Infrastructure.Core;
using Discount.Infrastructure.Extensions;
using Discount.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateDiscountCommandHandler>());

builder.Services.AddSingleton<IDapperContext, DapperContext>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddGrpc();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapGrpcService<DiscountService>();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(
        "Communication with gRPC endpoints must be made through a gRPC client.");
});

app.MigrateDatabase<Program>();

app.Run();
