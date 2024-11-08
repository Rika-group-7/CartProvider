namespace CartProvider.Models;

public class CartList
{
    public List<CartItem> Items { get; set;} = new List<CartItem>();

    public decimal TotalCost => Items.Sum(Items => Items.Price * Items.Quantity);

}
