using System.Data.Common;
using CCS.Rosetta.Api.Projects;
using Dapper;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = new SqliteConnection("Data Source=./database.db");
connection.Open();
connection.Execute("CREATE TABLE IF NOT EXISTS Projects (Name VARCHAR(255) PRIMARY KEY, Description TEXT);");
connection.Dispose();

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

namespace CCS.Rosetta.Api
{
    public partial class Program { }
}