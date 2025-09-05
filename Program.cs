using DotNetEnv;
using Microsoft.Extensions.Options;
using SistemaLogistics.Models;
using SistemaLogistics.Data;
//using SistemaLogistics.Services;

// Cargar variables de entorno AL INICIO
Env.Load();

var builder = WebApplication.CreateBuilder(args);


// Configurar MongoDB desde variables de entorno
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") ?? "mongodb://localhost:27017";
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME") ?? "LogisticsDB";
});

// Agregar servicios de MongoDB
builder.Services.AddSingleton<MongoDbContext>();

// Agregar servicios de aplicación
//builder.Services.AddScoped<UserService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    var env = Environment.GetEnvironmentVariable("ENVIRONMENT");
    var dbName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME");

    //Console de prueba 
    Console.WriteLine($"Ambiente: {env}");
    Console.WriteLine($"Base de datos: {dbName}");
    Console.WriteLine($"MongoDB Configurado correctamente");

    await next();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Prueba de la conexión al 
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
        Console.WriteLine(" Conexión a MongoDB exitosa!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($" Error conectando a MongoDB: {ex.Message}");
}

app.Run();
