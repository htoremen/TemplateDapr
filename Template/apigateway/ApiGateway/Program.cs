using Apigateway.Common.Behaviours;
using MediatR;
using MediatR.Pipeline;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using CacheManager.Core;
using Infrastructure.Application.Common.Behaviours;
using Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
builder.Services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));

//builder.Services.AddWebAuthentication(appSettings);

//builder.Services.AddCustomMVC(builder.Configuration);

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithRedisConfiguration("redis",
                config =>
                {
                    config.WithAllowAdmin()
                    .WithDatabase(0)
                    .WithEndpoint("localhost", 6379);
                })
        .WithJsonSerializer()
        .WithRedisCacheHandle("redis");
    });
        
//builder.Services.AddCustomJwtAuthentication(appSettings);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder => builder.WithOrigins("https://localhost:6010")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

var app = builder.Build();
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
