using StoreModels;
using Newtonsoft.Json;
using StorageEngine;
using System.Xml;

namespace CartProvider.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IStorageEngine _storageEngine;

    public ShoppingCartService(IStorageEngine storageEngine)
    {
        _storageEngine = storageEngine;
    }

    public bool TruncateShoppingCart(int userId)
    {

        return _storageEngine.TruncateShoppingCart(userId);
    }

    public ShoppingCart GetShoppingCart(int userId)
    {
        return _storageEngine.GetShoppingCart(userId);
    }

    public bool SaveShoppingCart(ShoppingCart shoppingCart)
    {
        return _storageEngine.SaveShoppingCart(shoppingCart);
    }
}
