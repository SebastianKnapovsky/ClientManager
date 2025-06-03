using ClientManager.API.Middleware;
using ClientManager.Core.Interfaces;
using ClientManager.Infrastructure.Data;
using ClientManager.Infrastructure.Repositories;
using ClientManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IPdfReportGenerator, PdfReportGenerator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy => policy
            .WithOrigins("http://localhost:5500") 
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowClient");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
