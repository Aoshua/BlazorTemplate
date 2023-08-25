using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeding;

public static class Seeder
{
    public static void Initialize(BackOfficeDbContext context, bool migrate, bool seed)
    {
        if (migrate)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        if (seed)
        {
            // ** Order is important here **
            new ProductSeeder(context).Seed();
        }
    }
}
