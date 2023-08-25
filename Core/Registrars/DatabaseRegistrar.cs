/*
 * NOTE:
 * 'using' on BuildServiceProvider() causes the app to fail at runtime on Azure App Service. 
 * Something about "Cannot access a disposed object. Object name: 'ConfigurationManager'." 
 * See https://github.com/dotnet/runtime/issues/66383 for more details.
 */

using Data.Context;
using Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Registrars;

public static class DatabaseRegistrar
{
    public static void RegisterDatabase(this IServiceCollection services, string connStr, bool migration, bool seed)
    {
        services.AddDbContext<BackOfficeDbContext>(x => x.UseSqlServer(connStr));

        var scope = services.BuildServiceProvider().CreateScope(); // See note at top of file
        using var context = scope.ServiceProvider.GetRequiredService<BackOfficeDbContext>();

        Seeder.Initialize(context, migration, seed);
    }
}
