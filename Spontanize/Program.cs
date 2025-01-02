using System.Text;
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

var connectionString = "Server=mysql;Port=3306;Database=spontanize;User=root;Password=password;";

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SpontanizeContext>();
    int retryCount = 3; // Aantal pogingen
    int delay = 2000;   // Wachtperiode in milliseconden

    for (int attempt = 1; attempt <= retryCount; attempt++)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("Database migrated successfully.");
            break; // Als het lukt, stoppen we de loop
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
            if (attempt == retryCount)
            {
                Console.WriteLine("All retry attempts failed.");
                throw; // Hergooi de uitzondering als het definitief niet lukt
            }
            Thread.Sleep(delay); // Wacht voordat je het opnieuw probeert
        }
    }
}


app.Use(async (context, next) =>
{
    var initialBody = context.Request.Body;

    using (var bodyReader = new StreamReader(context.Request.Body))
    {
        string body = await bodyReader.ReadToEndAsync();
        Console.WriteLine(body);
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
        await next.Invoke();
        context.Request.Body = initialBody;
    }
});

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.MapGroup("/api/auth").MapIdentityApi<User>();

app.Run();