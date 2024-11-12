using CartProvider.Services;
using Microsoft.AspNetCore.Mvc;
using StoreModels;

namespace CartProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet(Name = "GetShoppingCart")]
        public ShoppingCart Get(int userId)
        {
            var cart = _shoppingCartService.GetShoppingCart(userId);
            return cart;
        }

        [HttpPost(Name = "SaveShoppingCart")]
        public bool Save(ShoppingCart cart) 
        {
            _shoppingCartService.SaveShoppingCart(cart);
            return true;
        }

        [HttpPut(Name = "UpdateShoppingCart")]
        public bool Update(ShoppingCart cart)
        {
            _shoppingCartService.SaveShoppingCart(cart);
            return true;
        }

        [HttpDelete(Name = "DeleteShoppingCart")]
        public bool Delete(int userId) 
        {
            _shoppingCartService.TruncateShoppingCart(userId);
            return true;
        }
    }
}
