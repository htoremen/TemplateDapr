using HealthChecks.UI.Client;
using Identity.Service.Service;
using Infrastructure.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Services.AddApplication(appSettings);
builder.Services.AddInfrastructureService(appSettings);
builder.Services.AddWebUIServices(builder, appSettings);
builder.Services.AddHealthChecksServices(appSettings);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddCustomJwtAuthentication(appSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();

app.Run();
