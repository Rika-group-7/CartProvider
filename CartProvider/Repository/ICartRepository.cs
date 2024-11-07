using CartProvider.Models;

namespace CartProvider.Repository;

public interface ICartRepository
{
    Task<CartList> GetCart();
    Task<bool> AddCart(CartItem cartItem);
    Task<bool> UpdateQuantity(Guid productId, int newQuantity);
    Task<bool> DeleteQuantity(Guid productId);
}
