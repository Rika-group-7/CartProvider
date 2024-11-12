using StoreModels;

namespace StorageEngine;

public interface IStorageEngine
{
    ShoppingCart GetShoppingCart(int userId);
    bool SaveShoppingCart(ShoppingCart shoppingCart);
    bool TruncateShoppingCart(int userId);
}
