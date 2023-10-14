using Identity.Application;
using Identity.Infrastructure.Identity;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IMailService, MailService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        services.AddDbContext(appSettings);
        return services;
    }
    public static IServiceCollection AddInfrastructure2(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IMailService, MailService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        services.AddDbContext(appSettings);
        return services;
    }

    private static void AddDbContext(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                appSettings.ConnectionStrings.ConnectionString,
                configure =>
                {
                    configure.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    configure.EnableRetryOnFailure();
                }), ServiceLifetime.Scoped);

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }
}
