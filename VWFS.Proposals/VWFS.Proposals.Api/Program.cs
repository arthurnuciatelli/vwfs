using Microsoft.EntityFrameworkCore;
using VWFS.Proposals.Infrastructure.Persistence;
using VWFS.Proposals.Application.Services;
using VWFS.Proposals.Infrastructure.Messaging;
using VWFS.Proposals.Domain.Services.ProposalService;
using VWFS.Proposals.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var broker = builder.Configuration["KAFKA_BOOTSTRAP_SERVERS"] ?? "kafka:9092";

// PostgreSQL
builder.Services.AddDbContext<ProposalsDbContext>(options =>
    options.UseNpgsql(builder.Configuration["POSTGRES_CONNECTION"]));

// Repositório e Service
builder.Services.AddScoped<IProposalAppService, ProposalAppService>();
builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
builder.Services.AddSingleton<IProposalService, ProposalService>();

builder.Services.AddHostedService<KafkaConsumerService>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
