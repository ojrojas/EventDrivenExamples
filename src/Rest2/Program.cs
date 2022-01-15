using EventDrivenDesign.BuildingBlocks.EventBus.Abstractions;
using EventDrivenDesign.Rest2;
using EventDrivenDesign.Rest2.Application.IntegrationEvents;
using EventDrivenDesign.Rest2.Interfaces;
using EventDrivenDesign.Rest2.Mappers;
using EventDrivenDesign.Rest2.Repositories;
using EventDrivenDesign.Rest2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Rest2 Posts Api", Version = "v1" });
});
builder.Services.AddAutoMapper(typeof(PostProfile));
var Configuration = builder.Configuration;
builder.Services.RegisterEventBus(Configuration);
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddTransient<UserCreatedIntegrationEventHandler>();

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

