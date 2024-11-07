namespace CartProvider.Models;

public class CartItem
{
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
public class CartList
{
    public List<CartItem> Items { get; set;} = new List<CartItem>();

    public decimal TotalCost => Items.Sum(Items => Items.Price * Items.Quantity);

}
