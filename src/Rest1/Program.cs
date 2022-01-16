using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventDrivenDesign.Rest1;
using EventDrivenDesign.Rest1.Interfaces;
using EventDrivenDesign.Rest1.Mappers;
using EventDrivenDesign.Rest1.Respositories;
using EventDrivenDesign.Rest1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{ Title ="Rest1 Users Api", Version = "v1"});
});
builder.Services.AddAutoMapper(typeof(UserProfile));
var Configuration = builder.Configuration;
builder.Services.RegisterEventBus(Configuration);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.
// builder.Host.ConfigureContainer<ContainerBuilder>(builderOptions => builderOptions.Populate(builder.Services));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
