using LineManagement.API.Equipments;
using LineManagement.API.ProductionLines;
using LineManagement.Application;
using LineManagement.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LineManagement.API;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddLineManagement(this IHostApplicationBuilder builder)
    {
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        builder.Services.AddScoped<IProductionLineService, ProductionLineService>();
        builder.Services.AddScoped<IEquipmentService, EquipmentService>();

        return builder;
    }
}
