using System.ComponentModel.DataAnnotations;

namespace CartProvider.Models;

public class CartItem
{
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
