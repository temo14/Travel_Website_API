global using Altex_Task.Data;
global using Altex_Task;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Altex_Task.Models;
using Altex_Task.Models.Profile;
using Altex_Task.Exstensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISINtegration();
// Token configuration.

builder.Services.ConfigureJWT();

// Sql connection
var conncetionString = builder.Configuration.GetConnectionString("AltexAppCon");
//builder.Services.AddSqlite<DataContext>(conncetionString);

builder.Services.ConfigureMySqlContext(builder.Configuration);

// Configure Logger
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.ConfigureLoggerService();
// RepositryWrapper
builder.Services.ConfigureRepositoryWrapper();

//Add IdentityUser
//builder.Services.ConfigureIdentity<DataContext, UserModel>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
