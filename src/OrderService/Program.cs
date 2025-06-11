using MediatR;
using OrderService.Integration;
using OrderService.Messaging;
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

//Register IKafkaProducer
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

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
