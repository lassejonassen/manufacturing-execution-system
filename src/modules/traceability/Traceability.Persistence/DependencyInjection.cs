using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedKernel;
using SharedKernel.Persistence;
using Traceability.Domain.ConsumedMaterials.Repositories;
using Traceability.Domain.MaterialGenealogies.Repositories;
using Traceability.Domain.ProducedMaterials.Repositories;
using Traceability.Domain.ProductionRuns.Repositories;
using Traceability.Persistence.DbContexts;
using Traceability.Persistence.Repositories;

namespace Traceability.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Traceability");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Traceability Connection String is null or whitespace");
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
        services.AddScoped<IProductionRunRepository, ProductionRunRepository>(); 
        services.AddScoped<IConsumedMaterialRepository, ConsumedMaterialRepository>(); 
        services.AddScoped<IProducedMaterialRepository, ProducedMaterialRepository>();
        services.AddScoped<IMaterialGenealogyRepository, MaterialGenealogyRepository>();

        return services;
    }
}
