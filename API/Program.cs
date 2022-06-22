using AccountOwnerServer.Extensions;
using API.ActionFilters;
using Entities;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureMySqlContext<RepositoryContext>(builder.Configuration.GetConnectionString("AltexAppCon"));
builder.Services.ConfigureRepositoryWrapper();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.ConfigureJWT();
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddOptions();
builder.Services.AddAuthentication().AddCookie();

//builder.Services.AddAuthentication("BasicAuthentication")
//                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
//                ("BasicAuthentication", null);
//builder.Services.AddAuthorization();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
} 
else
    app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");


app.UseRouting();
//app.UseAuthentication();

app.UseMiddleware<API.Extensions.AuthenticationMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();