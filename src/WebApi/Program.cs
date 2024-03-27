using Carter;
using Persistence;
using Application;
using WebApi.Extensions;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Application.Orders.Commands.Create;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

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

app.MapCarter();
app.Run();

