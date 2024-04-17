using Carter;
using Persistence;
using Application;
using WebApi.Extensions;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Application.Orders.Commands.Create;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddRateLimiter(raterLimitierOptions =>
{
    raterLimitierOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    raterLimitierOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 3;
        options.QueueLimit = 3;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });

    raterLimitierOptions.AddSlidingWindowLimiter("sliding", options =>
    {
        options.Window = TimeSpan.FromSeconds(15);
        options.SegmentsPerWindow = 1;
        options.PermitLimit = 15;
    });

    raterLimitierOptions.AddTokenBucketLimiter("token", options =>
    {
        options.TokenLimit = 100;
        options.ReplenishmentPeriod = TimeSpan.FromSeconds(5);
        options.TokensPerPeriod = 10;
    });

    raterLimitierOptions.AddConcurrencyLimiter("concurrency", options =>
    {
        options.PermitLimit = 5;
    });
});

builder.Services.AddRebus(rebus => rebus
    .Routing(r =>
        r.TypeBased().MapAssemblyOf<ApplicationAssemblyReference>("eshop-queue"))
    .Transport(t =>
        t.UseRabbitMq(
            builder.Configuration.GetConnectionString("MessageBroker"), "eshop-queue"))
    .Sagas(s =>
        s.StoreInPostgres(
            builder.Configuration.GetConnectionString("Database"), "sagas", "saga_indexes")),
    onCreated: async bus =>
    {
        await bus.Subscribe<OrderConfirmationEmailSent>();
        await bus.Subscribe<OrderPaymentRequestSent>();
    });

builder.Services.AutoRegisterHandlersFromAssemblyOf<ApplicationAssemblyReference>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.MapCarter();
app.Run();

