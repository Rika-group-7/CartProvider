using CartProvider.Controllers;
using CartProvider.Services;
using StoreModels;
using StorageEngine;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Get_Shopping_Cart()
    {
        var user = new User() { UserId = 1, Email = "user1@example.com", Username = "user1" };
        var product = new Product() { Name = "product1", Price = 100, ProductId = 1 };
        var item = new ShoppingCartItem() { Product = product, Quantity = 5 };
        var items = new List<ShoppingCartItem>() { item };

        var cart = new ShoppingCart() { User = user, Items = items };
        Dictionary<int, ShoppingCart> carts = new Dictionary<int, ShoppingCart>();
        carts[user.UserId] = cart;

        var storageEngine = new MockStorageEngine(carts);
        var service = new ShoppingCartService(storageEngine);
        var controller = new ShoppingCartController(null, service);
        
        var result = controller.Get(user.UserId);
        Assert.IsNotNull(result);
        Assert.AreEqual(user, result.User);
        Assert.AreEqual((product.Price * item.Quantity), result.Total);
        Assert.AreEqual(items, result.Items);
    }

    [TestMethod]
    public void Save_Update_Shopping_Cart()
    {
        var user = new User() { UserId = 1, Email = "user1@example.com", Username = "user1" };
        var product = new Product() { Name = "product1", Price = 100, ProductId = 1 };
        var item = new ShoppingCartItem() { Product = product, Quantity = 5 };
        var items = new List<ShoppingCartItem>() { item };

        var cart = new ShoppingCart() { User = user, Items = items };
        Dictionary<int, ShoppingCart> carts = new Dictionary<int, ShoppingCart>();
        carts[user.UserId] = cart;

        var storageEngine = new MockStorageEngine(carts);
        var service = new ShoppingCartService(storageEngine);
        var controller = new ShoppingCartController(null, service);
        var result = controller.Get(user.UserId);

        Assert.AreEqual(product.ProductId, result.Items[0].Product.ProductId);
        Assert.AreEqual((product.Price * item.Quantity), result.Total);

        var product2 = new Product() { Name = "product2", Price = 50, ProductId = 2 };
        var item2 = new ShoppingCartItem() { Product = product2, Quantity = 2 };
        carts[user.UserId].Items.Add(item2);

        bool saveSuccess = controller.Save(carts[user.UserId]);
        Assert.IsTrue(saveSuccess);

        result = controller.Get(user.UserId);
        Assert.AreEqual(product2.ProductId, result.Items[1].Product.ProductId);
        var expectedProduct1Total = product.Price * item.Quantity;
        var expectedProduct2Total = product2.Price * item2.Quantity;

        Assert.AreEqual((expectedProduct1Total + expectedProduct2Total), result.Total);

        item.Quantity += 10;
        carts[user.UserId].Items[0] = item;

        controller.Update(carts[user.UserId]);

        Assert.AreEqual(item.Quantity, result.Items[0].Quantity);
        expectedProduct1Total = product.Price * item.Quantity;
        Assert.AreEqual((expectedProduct1Total + expectedProduct2Total), result.Total);
    }

    [TestMethod]
    public void Delete_Shopping_Cart()
    {
        var user = new User() { UserId = 1, Email = "user1@example.com", Username = "user1" };
        var product = new Product() { Name = "product1", Price = 100, ProductId = 1 };
        var item = new ShoppingCartItem() { Product = product, Quantity = 5 };
        var items = new List<ShoppingCartItem>() { item };

        var cart = new ShoppingCart() { User = user, Items = items };
        Dictionary<int, ShoppingCart> carts = new Dictionary<int, ShoppingCart>();
        carts[user.UserId] = cart;

        var storageEngine = new MockStorageEngine(carts);
        var service = new ShoppingCartService(storageEngine);
        var controller = new ShoppingCartController(null, service);
        
        bool deleteSuccess = controller.Delete(user.UserId);
        Assert.IsTrue(deleteSuccess);
        Assert.AreEqual(0, carts[user.UserId].Items.Count);
        Assert.AreEqual(0, carts[user.UserId].Total);
    }
}