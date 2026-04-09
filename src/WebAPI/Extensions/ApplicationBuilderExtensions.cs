using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions;

public static partial class ApplicationBuilderExtensions
{
    public static IApplicationBuilder MigrateDatabases(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        scope.ServiceProvider
            .GetRequiredService<LineManagement.Persistence.DbContexts.ApplicationDbContext>()
            .Database.Migrate();

        scope.ServiceProvider
            .GetRequiredService<Traceability.Persistence.DbContexts.ApplicationDbContext>()
            .Database.Migrate();

        return app;
    }
}
