using Core.Attributes;
using Microsoft.EntityFrameworkCore;
using Models.Products;
using Unify.Data.Repositories;

namespace Core.Modules.Projects;

[OnboardingModule]
public class ProductManager(Repository<Product> productRepo)
{
    public async Task<List<Product>> GetAllProducts()
        => await productRepo.GetAll();

    public async Task<List<Product>> GetAllProductsAndTiers()
        => await productRepo.Queryable()
            .Include(x => x.Tiers)
            .ToListAsync();
}
