using Data;
using Data.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Commands.Deals;
using Service.Pipeline;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddProblemDetails();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<SpontanizeContext>();

var connectionString = "Server=localhost;Database=spontanize;User=root;Password=;";

builder.Services.AddDbContext<SpontanizeContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var assemblies = AppDomain.CurrentDomain.GetAssemblies();

// Voeg de validators toe
builder.Services.AddValidatorsFromAssemblyContaining<CreateDealValidator>();
var serviceDescriptors = builder.Services
    .Where(descriptor => typeof(IValidator) != descriptor.ServiceType
                         && typeof(IValidator).IsAssignableFrom(descriptor.ServiceType)
                         && descriptor.ServiceType.IsInterface)
    .ToList();

foreach (var descriptor in serviceDescriptors)
{
    builder.Services.Add(new ServiceDescriptor(
        typeof(IValidator),
        p => p.GetRequiredService(descriptor.ServiceType),
        descriptor.Lifetime));
}


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies);
    cfg.AddOpenBehavior(typeof(ValidatorPipeline<,>));
});

builder.Services.AddTransient<IServiceHandler, ServiceHandler>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.MapGroup("/api/auth").MapIdentityApi<User>();

app.Run();