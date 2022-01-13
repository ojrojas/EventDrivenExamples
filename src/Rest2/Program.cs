using EventDrivenDesign.BuildingBlocks.EventBus.Abstractions;
using EventDrivenDesign.Rest2;
using EventDrivenDesign.Rest2.Application.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var Configuration = builder.Configuration;
builder.Services.RegisterEventBus(Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var eventBus = app.Services.GetRequiredService<EventDrivenDesign.BuildingBlocks.EventBus.Abstractions.IEventBus>();
eventBus.Subscribe<UserCreatedIntegrationEvent, IIntegrationEventHandler<UserCreatedIntegrationEvent>>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

