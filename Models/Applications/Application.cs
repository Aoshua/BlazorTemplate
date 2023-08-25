using Models.Common;
using Models.Products;

namespace Models.Applications;

public class Application : Entity
{
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public string Email { get; set; } = default!;

    public virtual List<Product> Products { get; set; } = new();
}
