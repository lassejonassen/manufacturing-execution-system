using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Messaging;

namespace Traceability.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatorHandlers(typeof(DependencyInjection).Assembly);

        return services;
    }
}
