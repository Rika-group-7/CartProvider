using CartProvider.DataContext;
using CartProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace CartProvider.Repository;

public class CartRepository(ApiContext context) : ICartRepository
{
    private readonly ApiContext _context = context;

    public async Task<CartList> GetCart()
    {
        var cartItem = await _context.DbCartItems.ToListAsync();
        return new CartList { Items = cartItem };
    }

    public async Task<bool> AddCart(CartItem cartItem)
    {
        var existingItem = await _context.DbCartItems.FirstOrDefaultAsync(e => e.ProductId == cartItem.ProductId);
        if (existingItem != null) 
        {
            existingItem.Quantity += cartItem.Quantity;
            _context.DbCartItems.Update(existingItem);
        }
        else
        {
            await _context.DbCartItems.AddAsync(cartItem);
        }
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateQuantity(Guid productId, int newQuantity)
    {


    }
    public async Task<bool> DeleteQuantity(Guid productId) 
    {

    }

}
