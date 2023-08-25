using System.ComponentModel.DataAnnotations;
using Models.Common;

namespace Models.Products;

public class ProductTier : Entity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    /// <summary>
    /// If null, unlimited seats
    /// </summary>
    public int? DocumentTypeSeats { get; set; }
}
