namespace StoreModels;

public class ShoppingCart
{
    public User User { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public decimal Total => Items.Sum(item => item.TotalPrice);
}

