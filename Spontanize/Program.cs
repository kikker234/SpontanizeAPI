using Data;
using Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<SpontanizeContext>();

var connectionString = "Server=localhost;Database=spontanize;User=root;Password=;";

builder.Services.AddDbContext<SpontanizeContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var assemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies);
});
builder.Services.AddTransient<IServiceHandler, ServiceHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapGroup("/api/auth").MapIdentityApi<User>();

app.Run();