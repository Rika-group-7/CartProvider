namespace StoreModels;

public class ShoppingCartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => Product.Price * Quantity;
}

