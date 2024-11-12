using StoreModels;

namespace CartProvider.Services;

public interface IShoppingCartService
{
    ShoppingCart GetShoppingCart(int userId);
    bool SaveShoppingCart(ShoppingCart shoppingCart);
    bool TruncateShoppingCart(int userId);
}
