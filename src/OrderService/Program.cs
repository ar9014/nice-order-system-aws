using MediatR;
using OrderService.Caching;
using OrderService.Integration;
using OrderService.Messaging;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Register the HTTP Client
builder.Services.AddHttpClient<INotificationClient, NotificationClient>();

// Register IKafkaProducer
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

// Regsiter Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<RedisCacheService>();

var app = builder.Build();

// Set custom port (5000)
app.Urls.Add("http://localhost:5000");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
