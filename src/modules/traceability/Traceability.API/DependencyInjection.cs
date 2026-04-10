using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Traceability.API.ConsumedMaterials;
using Traceability.API.ProductionRuns;
using Traceability.Application;
using Traceability.Persistence;

namespace Traceability.API;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddTraceability(this IHostApplicationBuilder builder)
    {
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        builder.Services.AddScoped<IProductionRunService, ProductionRunService>();
        builder.Services.AddScoped<IConsumedMaterialService, ConsumedMaterialService>();

        return builder;
    }
}
