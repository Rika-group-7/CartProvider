using StoreModels;

namespace StorageEngine;

public class MockStorageEngine : IStorageEngine
{
    public Dictionary<int, ShoppingCart> ShoppingCarts { get; private set; }
    public MockStorageEngine(Dictionary<int, ShoppingCart> shoppingCarts)
    {
        ShoppingCarts = shoppingCarts;
    }

    public ShoppingCart GetShoppingCart(int userId)
    {
        if (ShoppingCarts.ContainsKey(userId))
            return ShoppingCarts[userId];

        return null;
    }

    public bool SaveShoppingCart(ShoppingCart shoppingCart)
    {
        var userId = shoppingCart.User.UserId;
        if (ShoppingCarts.ContainsKey(userId))
            ShoppingCarts[userId] = shoppingCart;
        else
            ShoppingCarts.Add(userId, shoppingCart);

        return true;
    }

    public bool TruncateShoppingCart(int userId)
    {
        if (!ShoppingCarts.ContainsKey(userId))
            return false;

        ShoppingCarts[userId].Items.Clear();

        return true;
    }

    public static MockStorageEngine FromInitialMockData()
    {
        var user = new User() { UserId = 1, Email = "user1@example.com", Username = "user1" };
        var product = new Product() { Name = "product1", Price = 100, ProductId = 1 };
        var item = new ShoppingCartItem() { Product = product, Quantity = 5 };
        var items = new List<ShoppingCartItem>() { item };

        var cart = new ShoppingCart() { User = user, Items = items };
        Dictionary<int, ShoppingCart> carts = new Dictionary<int, ShoppingCart>();
        carts[user.UserId] = cart;

        return new MockStorageEngine(carts);
    }
}
