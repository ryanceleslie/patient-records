using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Reflection;
using Core.Services;
using Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// using secrets for local development
builder.Configuration.AddEnvironmentVariables()
                     .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

// Add services to the container.

builder.Services.AddDbContext<PatientRecordsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PatientRecordsConnection")));


// Core Services and infrastructure
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
