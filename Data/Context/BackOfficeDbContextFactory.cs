using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context;

/// <summary>
/// Used to run `dotnet ef ...` commands during development.
/// </summary>
public class BackOfficeDbContextFactory : IDesignTimeDbContextFactory<BackOfficeDbContext>
{
    public BackOfficeDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<BackOfficeDbContext>();

        // Pipe dream to load connection string from appsettings.json
        // Requires (Microsoft.Extensions.Configuration & Microsoft.Extensions.Configuration.Json)
        //IConfigurationRoot configuration = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .Build();

        //var connStr = configuration.GetConnectionString("Default");

        builder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackOffice;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        return new BackOfficeDbContext(builder.Options);
    }
}
