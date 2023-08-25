using Data.Context;
using Models.Products;

namespace Data.Seeding;
public class ProductSeeder(BackOfficeDbContext context)
{
    public void Seed()
    {
        if (!context.Products.Any())
        {
            context.Add(new Product()
            {
                Title = "ElementsXS",
                Description = "Asset Management and Civic Engagement",
                Tiers = new List<ProductTier>()
                {
                    new ProductTier()
                    {
                        Title = "Basic",
                        DocumentTypeSeats = 5
                    },
                    new ProductTier()
                    {
                        Title = "Standard",
                        DocumentTypeSeats = 15
                    },
                    new ProductTier()
                    {
                        Title = "Enterprise",
                        DocumentTypeSeats = null
                    }
                }
            });

            context.SaveChanges();
        }
    }
}
