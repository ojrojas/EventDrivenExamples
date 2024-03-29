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
builder.Services.AddMediatR(typeof(CreateUserHandler));
var Configuration = builder.Configuration;
builder.Services.RegisterEventBus(Configuration);
builder.Services.AddDbContext<Rest2DbContext>();
builder.Services.AddTransient<UserCreatedIntegrationEventHandler>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddTransient<UserCreatedIntegrationEventHandler>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builderOptions => builderOptions.RegisterModule<MediatorModule>());

builder.Services.AddTransient<InitializerDatabaseRest2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider;
    var initializer = service.GetRequiredService<InitializerDatabaseRest2>();
    initializer.Run();
    app.UseSwagger();
    app.UseSwaggerUI();
}

var eventBus = app.Services.GetRequiredService<EventDrivenDesign.BuildingBlocks.EventBus.Abstractions.IEventBus>();
eventBus.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // the order is important!!!!
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly);

         builder.RegisterType<Rest2DbContext>().InstancePerLifetimeScope();

        builder.RegisterType<UserRepository>()
       .As<IUserRepository>()
       .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(CreateUserCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

        builder.Register<MediatR.ServiceFactory>(context =>
       {
           var componentContext = context.Resolve<IComponentContext>();
           return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
       });
    }
}

