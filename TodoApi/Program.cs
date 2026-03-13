using TodoApi.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention()
);

// Corrigir timestamps
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// 📌 Adiciona Controllers + Swagger
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 📌 Swagger sempre habilitado (pode ajustar depois)
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers(); // <<<<<< IMPORTANTÍSSIMO

app.MapGet("/", () => "OK");

app.Run();