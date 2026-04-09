using Microsoft.Extensions.Hosting;
using Traceability.Application;
using Traceability.Persistence;

namespace Traceability.API;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddTraceability(this IHostApplicationBuilder builder)
    {
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        return builder;
    }
}
