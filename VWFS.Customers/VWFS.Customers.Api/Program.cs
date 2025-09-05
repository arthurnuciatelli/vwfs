using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using VWFS.Customers.Domain.Interfaces;
using VWFS.Customers.Infrastructure.Persistence;
using VWFS.Customers.Infrastructure.Messaging;
using MediatR;
using VWFS.Customers.Application.Handlers;
using Serilog;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();



// Log
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog();

// Registrar serializador de GUID para MongoDB
BsonSerializer.RegisterSerializer(new MongoDB.Bson.Serialization.Serializers.GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));

// Mongo
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration["MONGO_CONNECTION"]));
builder.Services.AddScoped(sp => sp.GetRequiredService<IMongoClient>().GetDatabase("vwfs_customer"));

// Repositories e Kafka
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Kafka
builder.Services.AddSingleton<KafkaProducer>(sp =>
{
    var bootstrapServers = builder.Configuration["KAFKA_BOOTSTRAP_SERVERS"];
    var logger = sp.GetRequiredService<ILogger<KafkaProducer>>();
    return new KafkaProducer(bootstrapServers, logger);
});
// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommandHandler).Assembly));

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseHttpMetrics();
app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics(); // Mapeia o endpoint /metrics
    endpoints.MapControllers();
});


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
