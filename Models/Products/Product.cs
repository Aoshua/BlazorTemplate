using System.ComponentModel.DataAnnotations;
using Models.Common;

namespace Models.Products;

public class Product : Entity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public virtual List<ProductTier> Tiers { get; set; } = new();
}
