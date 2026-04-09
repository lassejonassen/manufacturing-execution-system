using LineManagement.Domain.Equipments.Repositories;
using LineManagement.Domain.ProductionLines.Repositories;
using LineManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedKernel;
using SharedKernel.Persistence;
using Traceability.Persistence.DbContexts;

namespace Traceability.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Traceability");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Line Management Connection String is null or whitespace");
        }

        services.AddSingleton<SetUpdatedAtInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        {
            opt.UseSqlServer(connectionString, x =>
            {
                x.EnableRetryOnFailure();
                x.MigrationsHistoryTable("__EFMigrationsHistory");
            });

            if (sp.GetRequiredService<IHostEnvironment>().IsDevelopment())
            {
                opt.EnableSensitiveDataLogging();
            }

            opt.AddInterceptors(sp.GetRequiredService<SetUpdatedAtInterceptor>());
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IProductionLineRepository, ProductionLineRepository>(); 
        services.AddScoped<IEquipmentRepository, EquipmentRepository>(); 

        return services;
    }
}
